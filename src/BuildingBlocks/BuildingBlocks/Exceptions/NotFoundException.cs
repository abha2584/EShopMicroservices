
namespace BuildingBlocks.Exceptions
{
	public class NotFoundException : Exception
	{

		public NotFoundException(string message) : base(message)
		{
		}

		public NotFoundException(string message, object key) : base($"{message} (Key: {key})")
		{
		}
	}
}
