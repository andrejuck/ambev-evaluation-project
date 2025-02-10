using Ambev.DeveloperEvaluation.Common.Validation;
using Ambev.DeveloperEvaluation.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ambev.DeveloperEvaluation.Domain.Common;

public class BaseEntity : IComparable<BaseEntity>
{
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
    [NotMapped]
    protected readonly List<BaseDomainEvent<Sale>> _domainEvents = new();
    [NotMapped]
    public IReadOnlyCollection<BaseDomainEvent<Sale>> DomainEvents => _domainEvents.AsReadOnly();
    public void ClearDomainEvents() => _domainEvents.Clear();

    public Task<IEnumerable<ValidationErrorDetail>> ValidateAsync()
    {
        return Validator.ValidateAsync(this);
    }

    public int CompareTo(BaseEntity? other)
    {
        if (other == null)
        {
            return 1;
        }

        return other!.Id.CompareTo(Id);
    }

    protected virtual void SetUpdatedAt()
    {
        UpdatedAt = DateTime.UtcNow;
    }

    protected virtual void SetCreatedAt()
    {
        CreatedAt = DateTime.UtcNow;
    }

    protected virtual void SyncronizeChildrenCollection<T>(List<T> existingCollection, List<T> newCollection)
    {
        var toDelete = existingCollection.Except(newCollection);
        foreach (T item in newCollection)
        {
            T? val = existingCollection.SingleOrDefault((T e) => e.Equals(item));
            if (val == null)
            {
                existingCollection.Add(item);
            }
        }

        existingCollection.RemoveAll((T e) => toDelete.Contains(e));
    }
}