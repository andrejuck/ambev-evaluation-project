using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;
using OneOf.Types;

namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale
{
    public class UpdateSaleHandler : IRequestHandler<UpdateSaleCommand, UpdateSaleResult>
    {
        private readonly ISaleRepository _saleRepository;
        private readonly IProductRepository _productRepository;
        private readonly IBranchRepository _branchRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public UpdateSaleHandler(ISaleRepository saleRepository,
            IProductRepository productRepository,
            IBranchRepository branchRepository,
            ICustomerRepository customerRepository,
            IMapper mapper,
            IMediator mediator)
        {
            _saleRepository = saleRepository;
            _productRepository = productRepository;
            _branchRepository = branchRepository;
            _customerRepository = customerRepository;
            _mapper = mapper;
            _mediator = mediator;
        }

        public async Task<UpdateSaleResult> Handle(UpdateSaleCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateSaleValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            var existingSale = await _saleRepository.GetByIdAsync(request.Id, cancellationToken);
            if (existingSale == null)
                throw new KeyNotFoundException($"Sale with ID {request.Id} not found");

            existingSale.BindCustomer(await _customerRepository.GetByIdAsync(request.Customer.Id, cancellationToken) ?? _mapper.Map<Customer>(request.Customer));
            existingSale.BindBranch(await _branchRepository.GetByIdAsync(request.Branch.Id, cancellationToken) ?? _mapper.Map<Branch>(request.Branch));

            var updatedSaleItems = _mapper.Map(request.Items, existingSale.Items);
            var existingProducts = await _productRepository.GetProductsByIdsAsync(updatedSaleItems.Select(x => x.Product.Id).ToList());
            foreach (var item in updatedSaleItems)
            {
                var existingProduct = existingProducts.FirstOrDefault(p => p.Id == item.Product.Id);
                if (existingProduct == null)
                    throw new ValidationException($"Product with ID {item.Product.Id} not found.");

                item.BindProduct(existingProduct);
            }

            existingSale.PrepareForUpdate(request.SaleDate, request.SaleStatus, updatedSaleItems);
            var updatedSale = await _saleRepository.UpdateAsync(existingSale, cancellationToken);

            if (updatedSale != null)
            {
                foreach (var domainEvent in existingSale.DomainEvents)
                {
                    await _mediator.Send(domainEvent);
                }

                existingSale.ClearDomainEvents();
            }

            var result = _mapper.Map<UpdateSaleResult>(updatedSale);
            return result;
        }
    }
}
