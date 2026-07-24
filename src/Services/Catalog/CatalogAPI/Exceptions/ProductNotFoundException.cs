using BuildingBlocks.Exceptions;

namespace CatalogAPI.Exceptions
{
	public class ProductNotFoundException : NotFoundException
	{
		public ProductNotFoundException(Guid productId) : base("Product", productId)
		{
		}
	}
}
