using Ambev.DeveloperEvaluation.Application.Branches.DTOs;
using Ambev.DeveloperEvaluation.Application.Customers.DTOs;
using Ambev.DeveloperEvaluation.Application.Products.DTOs;
using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Application.Sales.DTOs;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Bogus;

public static class CreateSaleHandlerTestData
{
    private static readonly Faker<CreateSaleCommand> createSaleHandlerFaker = new Faker<CreateSaleCommand>()
        .RuleFor(s => s.Customer, f => new CustomerDTO { Id = Guid.NewGuid(), Name = f.Name.FullName() })
        .RuleFor(s => s.Branch, f => new BranchDTO { Id = Guid.NewGuid(), Name = f.Company.CompanyName() })
        .RuleFor(s => s.Items, f => new List<SaleItemDTO>
        {
            new SaleItemDTO
            {
                Product = new ProductDTO { Id = Guid.NewGuid(), Name = f.Commerce.ProductName(), UnitPrice = f.Random.Decimal(1, 1000) },
                Quantity = f.Random.Int(1, 10)
            }
        });

    public static CreateSaleCommand GenerateValidCommand()
    {
        var customer = new Customer("Customer Name") { Id = Guid.NewGuid() };
        var branch = new Branch("Branch Name") { Id = Guid.NewGuid() };
        var product = new Product(new Random().Next(1000, 9999), "product name", Convert.ToDecimal(new Random().NextDouble())) {  Id = Guid.NewGuid()};
        var items = new List<SaleItem> { new SaleItem(product, new Random().Next(1, 10)) };
        var sale = new Sale(DateTime.UtcNow, customer, branch, items);

        return GenerateDto(customer, branch, items);
    }

    public static CreateSaleCommand GenerateDto(Customer customer, Branch branch, List<SaleItem> items)
    {
        return new CreateSaleCommand
        {
            Customer = new CustomerDTO { Id = customer.Id, Name = customer.Name },
            Branch = new BranchDTO { Id = branch.Id, Name = branch.Name },
            Items = new List<SaleItemDTO>(
                items.Select(x => new SaleItemDTO
                {
                    Product = new ProductDTO { Id = x.Product.Id, Name = x.Product.Name, UnitPrice = x.Product.UnitPrice },
                    Quantity = items[0].Quantity
                })
            )
        };
    }
}