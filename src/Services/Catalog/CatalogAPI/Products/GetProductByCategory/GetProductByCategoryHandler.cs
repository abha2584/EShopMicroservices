
using CatalogAPI.Models;

namespace CatalogAPI.Products.GetProductByCategory
{
	public record GetProductByCategoryQuery(string Category) : IQuery<GetProductByCategoryResult>;

	public record GetProductByCategoryResult(IEnumerable<Product> Products);
	internal class GetProductByCategoryQueryHandler : IQueryHandler<GetProductByCategoryQuery, GetProductByCategoryResult>
	{
		private readonly IDocumentSession _session;
		//private readonly ILogger<GetProductByCategoryQueryHandler> _logger;
		public GetProductByCategoryQueryHandler(IDocumentSession session)
		{
			_session = session;
			//_logger = logger;
		}
		public async Task<GetProductByCategoryResult> Handle(GetProductByCategoryQuery query, CancellationToken cancellationToken)
		{
			//_logger.LogInformation("Handling GetProductByCategoryQuery");
			var products = await _session.Query<Product>()
			   .Where(p => p.Category.Contains(query.Category)).ToListAsync(token: cancellationToken);
			return new GetProductByCategoryResult(products);

		}
	}
}
