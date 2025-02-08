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
        private readonly IMapper _mapper;

        public CreateSaleHandler(ISaleRepository saleRepository, IProductRepository productRepository, IMapper mapper)
        {
            _saleRepository = saleRepository;
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<CreateSaleResult> Handle(CreateSaleCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateSaleCommandValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            var sale = _mapper.Map<Sale>(request);

            var productIds = request.Items.Select(i => i.Product.Id).ToList();
            var existingProducts = await _productRepository.GetProductsByIdsAsync(productIds);

            foreach (var item in sale.Items)
            {
                var existingProduct = existingProducts.FirstOrDefault(p => p.Id == item.Product.Id);
                if (existingProduct == null)
                    throw new ValidationException($"Produto com ID {item.Product.Id} não encontrado.");

                item.BindProduct(existingProduct);
            }

            var createSale = await _saleRepository.CreateAsync(sale, cancellationToken);
            var result = _mapper.Map<CreateSaleResult>(createSale);
            return result;
        }
    }
}
