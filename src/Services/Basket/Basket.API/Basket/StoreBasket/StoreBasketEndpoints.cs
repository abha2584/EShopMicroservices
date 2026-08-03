namespace Basket.API.Basket.StoreBasket
{
	public record StoreBasketRequest(ShoppingCart cart);

	public record StoreBasketResponse(string UserName);
	public class StoreBasketEndpoints : ICarterModule
	{
		public void AddRoutes(IEndpointRouteBuilder app)
		{
			app.MapPost("/basket", async (StoreBasketRequest request, ISender sender) =>
			{
				var command = new StoreBasketCommand(request.cart);
				var result = await sender.Send(command);
				var response = result.Adapt<StoreBasketResponse>();
				return Results.Created($"/basket/{response.UserName}", response);
			})
			.WithName("StoreBasket")
			.Produces<StoreBasketResponse>(StatusCodes.Status200OK)
			.ProducesProblem(StatusCodes.Status400BadRequest)
			.WithSummary("Store a basket")
			.WithDescription("Store a basket");

		}
	}
}
