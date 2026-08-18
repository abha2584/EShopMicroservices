

namespace Ordering.Domain.Abstraction
{
	public class Aggregate<TId> : Entity<TId>, IAggregate<TId>
	{
		private readonly List<IDomainEvent> _domainEvents = new();
		public IReadOnlyList<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

		IReadOnlyList<IDomainEvent> IAggregate.DomainEvents { get => DomainEvents; set => throw new NotImplementedException(); }

		public IDomainEvent[] ClearDomainEvents()
		{
			IDomainEvent[] events = _domainEvents.ToArray();
			_domainEvents.Clear();
			return events;
		}

		public void AddDomainEvent(IDomainEvent domainEvent)
		{
			_domainEvents.Add(domainEvent);
		}
	}
}
