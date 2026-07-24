namespace CatalogAPI.Products.UpdateProduct
{
	public record UpdateProductRequest(Guid Id, string Name, List<string> Category, string Description, string ImageFile, decimal Price);

	public record UpdateProductResponse(Guid Id);
	public class UpdateProductEndpoint :ICarterModule
	{
		public void AddRoutes(IEndpointRouteBuilder app)
		{
			app.MapPut("/products/{id}", async (Guid id, UpdateProductRequest request, ISender sender) =>
			{
				var command = new UpdateProductCommand(id, request.Name, request.Category, request.Description, request.ImageFile, request.Price);
				var result = await sender.Send(command);
				var response = result.Adapt<UpdateProductResponse>();
				return Results.Ok(response);
			}).WithDescription("UpdateProduct")
			.Produces<UpdateProductResponse>(StatusCodes.Status200OK)
			.ProducesProblem(StatusCodes.Status400BadRequest)
			.WithSummary("Update an existing product")
			.WithName("UpdateProduct");
		}
	}
}
