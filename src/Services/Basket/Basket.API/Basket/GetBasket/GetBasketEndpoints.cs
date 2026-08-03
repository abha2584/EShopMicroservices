


namespace Basket.API.Basket.GetBasket
{
	//public record GetBasketRequest(string UserName);

	public record GetBasketResponse(ShoppingCart Cart);
	public class GetBasketEndpoints : ICarterModule
	{
		public void AddRoutes(IEndpointRouteBuilder app)
		{
			app.MapGet("/basket/{userName}", async (string userName, ISender sender) =>
			{
				var query = new GetBasketQuery(userName);
				var result = await sender.Send(query);
				var response = result.Adapt<GetBasketResponse>();
				return Results.Ok(response);
			})
			.WithName("GetBasketByID")
			.Produces<GetBasketResponse>(StatusCodes.Status200OK)
			.ProducesProblem(StatusCodes.Status404NotFound)
			.WithSummary("Get a basket by user name")
			.WithDescription("Get a basket by user name");
			
		}
	}
}
