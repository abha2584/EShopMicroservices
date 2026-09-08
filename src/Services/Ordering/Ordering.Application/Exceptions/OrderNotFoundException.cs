

using BuildingBlocks.Exceptions;

namespace Ordering.Application.Exceptions
{
	public class OrderNotFoundException : NotFoundException
	{
		public OrderNotFoundException(Guid orderId) : base($"Order with id: {orderId} was not found.")
		{
		}
	}
}
