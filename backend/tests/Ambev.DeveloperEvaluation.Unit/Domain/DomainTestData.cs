using Ambev.DeveloperEvaluation.Domain.Entities;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Domain
{
    public static class DomainTestData
    {

        public static Faker<Sale> GenerateSaleFaker()
        {
            var customerFaker = new Faker<Customer>()
                .CustomInstantiator(f => new Customer(f.Name.FullName()) { Id = Guid.NewGuid() });

            var branchFaker = new Faker<Branch>()
                .CustomInstantiator(f => new Branch(f.Company.CompanyName()) { Id = Guid.NewGuid() });

            var productFaker = new Faker<Product>()
                .CustomInstantiator(f => new Product(f.Random.Int(1000, 9999), f.Commerce.ProductName(), f.Random.Decimal(1, 1000)) {  Id = Guid.NewGuid() });

            var saleItemFaker = new Faker<SaleItem>()
                .CustomInstantiator(f =>
                {
                    var product = productFaker.Generate();
                    return new SaleItem(product, f.Random.Int(1, 10)) { Id = Guid.NewGuid() };
                });

            var saleFaker = new Faker<Sale>()
                .CustomInstantiator(f =>
                {
                    var customer = customerFaker.Generate();
                    var branch = branchFaker.Generate();
                    var items = saleItemFaker.Generate(f.Random.Int(1, 5));

                    return new Sale(DateTime.UtcNow, customer, branch, items);
                });

            return saleFaker;
        }

        public static Sale GenerateValidSale() => GenerateSaleFaker().Generate();

        public static Faker<Product> GenerateProductFaker()
        {
            return new Faker<Product>()
                .CustomInstantiator(f => new Product(
                    f.Random.Int(1000, 9999),
                    f.Commerce.ProductName(),
                    f.Random.Decimal(1, 1000)
                ));
        }

        public static Product GenerateValidProduct() => GenerateProductFaker().Generate();


        public static Faker<Branch> GenerateBranchFaker()
        {
            return new Faker<Branch>()
                .CustomInstantiator(f => new Branch(f.Company.CompanyName()));
        }

        public static Branch GenerateValidBranch() => GenerateBranchFaker().Generate();

        public static Faker<Customer> GenerateCustomerFaker()
        {
            return new Faker<Customer>()
                .CustomInstantiator(f => new Customer(f.Name.FullName()));
        }

        public static Customer GenerateValidCustomer() => GenerateCustomerFaker().Generate();
    }
}
