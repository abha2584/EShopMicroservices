namespace CatalogAPI.Products.GetProductByCategory
{
	// This class is intentionally left empty to serve as a placeholder for the GetProductByCategory endpoint.

	public record GetProductByCategoryResponse(IEnumerable<Product> Products);
	public class GetProductByCategoryEndpoint : ICarterModule
	{
		public void AddRoutes(IEndpointRouteBuilder app)
		{
			app.MapGet("/products/category/{Category}", async (string Category, ISender sender) =>
			{
				var query = new GetProductByCategoryQuery(Category);
				var result = await sender.Send(query);
				var response = result.Adapt<GetProductByCategoryResponse>();
				return Results.Ok(response);
			}).WithDescription("GetProductByCategory")
			.Produces<GetProductByCategoryResponse>(StatusCodes.Status200OK)
				.ProducesProblem(StatusCodes.Status400BadRequest)
				.WithSummary("Get products by category")
				.WithName("GetProductByCategory");
		}
	}
}
