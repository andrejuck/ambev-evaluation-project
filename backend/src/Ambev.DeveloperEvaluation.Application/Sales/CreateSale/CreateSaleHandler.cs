using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;


namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale
{
    public class CreateSaleHandler : IRequestHandler<CreateSaleCommand, CreateSaleResult>
    {
        private readonly ISaleRepository _saleRepository;
        private readonly IProductRepository _productRepository;
        private readonly IBranchRepository _branchRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;

        public CreateSaleHandler(ISaleRepository saleRepository, 
            IProductRepository productRepository, 
            IBranchRepository branchRepository, 
            ICustomerRepository customerRepository,
            IMapper mapper) 
        {
            _saleRepository = saleRepository;
            _productRepository = productRepository;
            _branchRepository = branchRepository;
            _customerRepository = customerRepository;
            _mapper = mapper;
        }

        public async Task<CreateSaleResult> Handle(CreateSaleCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateSaleCommandValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            var sale = _mapper.Map<Sale>(request);

            sale.BindCustomer(await _customerRepository.GetByIdAsync(request.Customer.Id, cancellationToken) ?? _mapper.Map<Customer>(request.Customer));
            sale.BindBranch(await _branchRepository.GetByIdAsync(request.Branch.Id, cancellationToken) ?? _mapper.Map<Branch>(request.Branch));

            var productIds = request.Items.Select(i => i.Product.Id).ToList();
            var existingProducts = await _productRepository.GetProductsByIdsAsync(productIds);
            foreach (var item in sale.Items)
            {
                var existingProduct = existingProducts.FirstOrDefault(p => p.Id == item.Product.Id);
                if (existingProduct == null)
                    throw new ValidationException($"Product with ID {item.Product.Id} not found.");

                item.BindProduct(existingProduct);
            }

            var createSale = await _saleRepository.CreateAsync(sale, cancellationToken);
            var result = _mapper.Map<CreateSaleResult>(createSale);
            return result;
        }
    }
}
