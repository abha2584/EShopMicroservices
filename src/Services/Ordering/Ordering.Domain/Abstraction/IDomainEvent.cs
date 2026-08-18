using MediatR;

namespace Ordering.Domain.Abstraction
{
	public interface IDomainEvent : INotification
	{
		Guid EventId => Guid.NewGuid();

		public DateTime occurredOn => DateTime.Now;

		public string EventType => GetType().AssemblyQualifiedName;
	}
}
