using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Unit.Domain;
using AutoMapper;
using FluentAssertions;
using FluentValidation;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Sale.CreateSale;

public class CreateSaleHandlerTests
{
    private readonly ISaleRepository _saleRepository;
    private readonly IProductRepository _productRepository;
    private readonly IBranchRepository _branchRepository;
    private readonly ICustomerRepository _customerRepository;
    private readonly IMapper _mapper;
    private readonly CreateSaleHandler _handler;

    public CreateSaleHandlerTests()
    {
        _saleRepository = Substitute.For<ISaleRepository>();
        _productRepository = Substitute.For<IProductRepository>();
        _branchRepository = Substitute.For<IBranchRepository>();
        _customerRepository = Substitute.For<ICustomerRepository>();
        _mapper = Substitute.For<IMapper>();

        _handler = new CreateSaleHandler(_saleRepository, _productRepository, _branchRepository, _customerRepository, _mapper);
    }

    [Fact(DisplayName = "Given valid sale data When creating sale Then returns success response")]
    public async Task Handle_ValidRequest_ReturnsSuccessResponse()
    {
        // Given
        var command = CreateSaleHandlerTestData.GenerateValidCommand();
        var sale = DomainTestData.GenerateValidSale();
        var result = new CreateSaleResult { Id = sale.Id };

        _mapper.Map<DeveloperEvaluation.Domain.Entities.Sale>(command).Returns(sale);
        _mapper.Map<CreateSaleResult>(sale).Returns(result);
        _saleRepository.CreateAsync(Arg.Any<DeveloperEvaluation.Domain.Entities.Sale>(), Arg.Any<CancellationToken>()).Returns(sale);
        _productRepository.GetProductsByIdsAsync(Arg.Any<List<Guid>>()).Returns(sale.Items.Select(x => x.Product));
        _customerRepository.GetByIdAsync(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(DomainTestData.GenerateValidCustomer());
        _branchRepository.GetByIdAsync(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(DomainTestData.GenerateValidBranch());

        // When
        var createSaleResult = await _handler.Handle(command, CancellationToken.None);

        // Then
        createSaleResult.Should().NotBeNull();
        createSaleResult.Id.Should().Be(sale.Id);
        await _saleRepository.Received(1).CreateAsync(Arg.Any<DeveloperEvaluation.Domain.Entities.Sale>(), Arg.Any<CancellationToken>());
    }

    [Fact(DisplayName = "Given invalid sale data When creating sale Then throws validation exception")]
    public async Task Handle_InvalidRequest_ThrowsValidationException()
    {
        // Given
        var command = new CreateSaleCommand();

        // When
        var act = () => _handler.Handle(command, CancellationToken.None);

        // Then
        await act.Should().ThrowAsync<ValidationException>();
    }

    [Fact(DisplayName = "Given sale request When handling Then binds existing customer and branch")]
    public async Task Handle_ValidRequest_BindsCustomerAndBranch()
    {
        // Given
        var sale = DomainTestData.GenerateValidSale();
        var customer = DomainTestData.GenerateValidCustomer();
        var branch = DomainTestData.GenerateValidBranch();
        var product = DomainTestData.GenerateValidProduct();
        var command = CreateSaleHandlerTestData.GenerateDto(customer, branch, sale.Items);


        _mapper.Map<DeveloperEvaluation.Domain.Entities.Sale>(command).Returns(sale);
        _customerRepository.GetByIdAsync(command.Customer.Id, Arg.Any<CancellationToken>()).Returns(customer);
        _branchRepository.GetByIdAsync(command.Branch.Id, Arg.Any<CancellationToken>()).Returns(branch);
        _productRepository.GetProductsByIdsAsync(Arg.Any<List<Guid>>()).Returns(sale.Items.Select(x => x.Product));
        _saleRepository.CreateAsync(Arg.Any<DeveloperEvaluation.Domain.Entities.Sale>(), Arg.Any<CancellationToken>()).Returns(sale);

        // When
        await _handler.Handle(command, CancellationToken.None);

        // Then
        sale.Customer.Should().Be(customer);
        sale.Branch.Should().Be(branch);
    }

    [Fact(DisplayName = "Given sale request with non-existing product When handling Then throws validation exception")]
    public async Task Handle_RequestWithNonExistingProduct_ThrowsValidationException()
    {
        // Given
        var command = CreateSaleHandlerTestData.GenerateValidCommand();
        var sale = DomainTestData.GenerateValidSale();

        _mapper.Map<DeveloperEvaluation.Domain.Entities.Sale>(command).Returns(sale);
        _productRepository.GetProductsByIdsAsync(Arg.Any<List<Guid>>()).Returns(new List<Product>()); // No products found

        // When
        var act = () => _handler.Handle(command, CancellationToken.None);

        // Then
        await act.Should().ThrowAsync<ValidationException>().WithMessage("*Product with ID*");
    }
}
