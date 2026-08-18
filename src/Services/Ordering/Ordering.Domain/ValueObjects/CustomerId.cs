

namespace Ordering.Domain.ValueObjects
{
	public class CustomerId
	{
		public Guid Value { get; }

		private CustomerId(Guid value) => Value = value;	

		public static CustomerId Of(Guid value)
		{
			ArgumentNullException.ThrowIfNull(value, nameof(value));
			if(value == Guid.Empty)
			{
				throw new CannotUnloadAppDomainException("Customer Id cannot be empty");
			}
			return new CustomerId(value);
		}

	}
}
