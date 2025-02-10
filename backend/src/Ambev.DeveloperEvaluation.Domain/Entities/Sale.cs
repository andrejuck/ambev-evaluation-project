
using Ambev.DeveloperEvaluation.Common.Validation;
using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.Validation;

namespace Ambev.DeveloperEvaluation.Domain.Entities
{
    public class Sale : BaseEntity
    {
        private Sale()
        {
            SetCreatedAt();
            CalculateTotalAmount();
        }

        public Sale(DateTime saleDate,
            Customer customer,
            Branch branch,
            List<SaleItem> items)
        {
            SaleDate = saleDate;
            Customer = customer;
            Branch = branch;
            Items = items;
            SetCreatedAt();
            CalculateTotalAmount();
        }

        public int SaleNumber { get; }
        public DateTime SaleDate { get; private set; } = DateTime.UtcNow;
        public Customer Customer { get; private set; }
        public decimal TotalAmount { get; private set; }
        public Branch Branch { get; private set; }
        public List<SaleItem> Items { get; private set; } = new List<SaleItem>();
        public SaleStatus SaleStatus { get; private set; } = SaleStatus.Pending;

        public ValidationResultDetail Validate()
        {
            var validator = new SaleValidator();
            var result = validator.Validate(this);
            return new ValidationResultDetail
            {
                IsValid = result.IsValid,
                Errors = result.Errors.Select(o => (ValidationErrorDetail)o)
            };
        }

        public void Approve()
        {
            SaleStatus = SaleStatus.Approved;
            SetUpdatedAt();
        }

        public void Cancel()
        {
            SaleStatus = SaleStatus.Cancelled;
            Items.ForEach(item => item.CancelItem());
            SetUpdatedAt();
        }

        public void PrepareForUpdate(DateTime newSaleDate, List<SaleItem> items)
        {
            SyncronizeChildrenCollection(this.Items, items);
            SetUpdatedAt();
            CalculateTotalAmount();
        }

        public void BindCustomer(Customer newCustomer)
        {
            Customer = newCustomer;
        }

        public void BindBranch(Branch newBranch)
        {
            Branch = newBranch;
        }

        public void CalculateTotalAmount()
        {
            TotalAmount = Items.Sum(x => x.TotalAmount);
        }
    }
}
