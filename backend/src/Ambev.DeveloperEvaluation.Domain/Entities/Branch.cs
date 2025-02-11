using Ambev.DeveloperEvaluation.Domain.Common;

namespace Ambev.DeveloperEvaluation.Domain.Entities
{
    public class Branch : BaseEntity
    {
        private Branch() { }
        public Branch(string name)
        {
            Name = name;
            SetCreatedAt();
        }

        public string Name { get; private set; } = string.Empty;
    }
}
