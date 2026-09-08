using Ordering.Application.Orders.Queries.GetOrdersByName;

namespace Ordering.API.Endpoints
{
	public record GetOrdersByNameResponse(IEnumerable<OrderDto> orders);
	public class GetOrdersByName :ICarterModule
	{
		public void AddRoutes(IEndpointRouteBuilder app)
		{
			app.MapGet("/orders/{orderName}", async (string orderName, ISender sender) =>
			{
				var query = new GetOrdersByNameQuery(orderName);
				var result = await sender.Send(query);
				var response = result.Adapt<GetOrdersByNameResponse>();
				return Results.Ok(response);
			})
			.WithName("GetOrdersByName")
			.WithDescription("Get Orders By User Name")
			.WithSummary("Get Orders By User Name")
			.Produces<GetOrdersByNameResponse>(StatusCodes.Status200OK)
			.ProducesProblem(StatusCodes.Status400BadRequest);
		}
	}
}
