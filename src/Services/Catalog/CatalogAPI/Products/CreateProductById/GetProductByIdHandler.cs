
using CatalogAPI.Products.GetProducts;

namespace CatalogAPI.Products.CreateProductById
{
	public record GetProductByIdQuery(Guid ProductId) : IQuery<GetProductByIdResult>;

	public record GetProductByIdResult(Product Product);
	internal class GetProductByIdQueryHandler : IQueryHandler<GetProductByIdQuery, GetProductByIdResult>
	{

		private readonly IDocumentSession _session;
		private readonly ILogger<GetProductsQueryHandler> _logger;
		public GetProductByIdQueryHandler(IDocumentSession session , ILogger<GetProductsQueryHandler> logger) { 
			_session = session;
			_logger = logger;
		}

		public async Task<GetProductByIdResult> Handle(GetProductByIdQuery query, CancellationToken cancellationToken)
		{
			_logger.LogInformation("Handling GetProductByIdQuery for ProductId: {ProductId}", query.ProductId);
			var product = await _session.LoadAsync<Product>(query.ProductId , cancellationToken);
			if(product == null)
			{
				new ProductNotFoundException(query.ProductId);
			}

			return new GetProductByIdResult(product);
		}
	}
}
