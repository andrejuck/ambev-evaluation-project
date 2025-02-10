

namespace Ambev.DeveloperEvaluation.Domain.Common
{
    public interface IDomainEvent<T>
    {
        Guid EventId { get; }
        EventType EventType { get; }
        T Entity { get; }
    }
}
