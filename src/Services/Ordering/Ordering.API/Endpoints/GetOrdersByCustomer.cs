
using Ordering.Application.Orders.Queries.GetOrdersByCustomer;

namespace Ordering.API.Endpoints
{
	public record GetOrdersByCustomerResponse(IEnumerable<OrderDto> orders);
	public class GetOrdersByCustomer : ICarterModule
	{
		public void AddRoutes(IEndpointRouteBuilder app)
		{
			app.MapGet("/orders/customer/{customerId}", async (Guid customerId, ISender sender) =>
			{
				var query = new GetOrdersByCustomerQuery(customerId);
				var result = await sender.Send(query);
				var response = result.Adapt<GetOrdersByCustomerResponse>();
				return Results.Ok(response);
			})
				.WithName("GetOrdersByCustomer")
				.WithDescription("Get Orders By Customer Id")
				.WithSummary("Get Orders By Customer Id")
				.Produces<GetOrdersByCustomerResponse>(StatusCodes.Status200OK)
				.ProducesProblem(StatusCodes.Status400BadRequest);
		}
	}
}
