using MediatR;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Ambev.DeveloperEvaluation.Domain.Common
{
    public class BaseDomainEvent<T> : IDomainEvent<T>
    {
        public Guid EventId { get; protected set; }

        public EventType EventType { get; protected set; }

        public T Entity { get; protected set; }

        protected virtual string EntityToJsonString() 
            => JsonSerializer.Serialize(Entity);

        public virtual string EventMessage()
            => string.Format("EventType: {0} | EventId: {1} |  Entity: {2}", EventType, EventId, EntityToJsonString());
        
    }
}
