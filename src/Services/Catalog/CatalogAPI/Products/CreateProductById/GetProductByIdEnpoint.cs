
namespace CatalogAPI.Products.CreateProductById
{
	// This class is intentionally left empty as a placeholder for the GetProductById endpoint implementation.
	public record GetProductByIdRequest(Product Product);
	public class GetProductByIdEnpoint : ICarterModule
	{
		public void AddRoutes(IEndpointRouteBuilder app)
		{
			app.MapGet("/products/{ProductId:guid}", async (Guid ProductId, ISender sender) =>
			{
				var query = new GetProductByIdQuery(ProductId);
				var result = await sender.Send(query);
				var response = result.Adapt<GetProductByIdRequest>();
				return Results.Ok(response);
			}).WithDescription("GetProductById")
			.Produces<GetProductByIdRequest>(StatusCodes.Status200OK)
				.ProducesProblem(StatusCodes.Status400BadRequest)
				.WithSummary("Get product by Id")
				.WithName("GetProductById");
		}
	}
}
