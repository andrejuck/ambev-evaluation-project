using Ambev.DeveloperEvaluation.Domain.Common;
namespace Ambev.DeveloperEvaluation.Domain.Entities
{
    public class Customer : BaseEntity
    {
        public Customer(string name)
        {
            Name = name;
            SetCreatedAt();
        }

        public string Name { get; private set; } = string.Empty;
    }
}
