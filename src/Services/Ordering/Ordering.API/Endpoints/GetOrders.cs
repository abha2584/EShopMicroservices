using BuildingBlocks.Pagination;
using Ordering.Application.Orders.Queries.GetOrders;

namespace Ordering.API.Endpoints
{

	public record GetOrdersResponse(PaginatedResult<OrderDto> orders);
	public class GetOrders : ICarterModule
	{
		public void AddRoutes(IEndpointRouteBuilder app)
		{
			app.MapGet("/orders", async ([AsParameters] PaginationRequest request, ISender sender) =>
			{
				var query = new GetOrdersQuery(request);
				var result = await sender.Send(query);
				var response = result.Adapt<GetOrdersResponse>();
				return Results.Ok(response);
			})
				.WithName("GetOrders")
				.WithDescription("Get Orders")
				.WithSummary("Get Orders")
				.Produces<GetOrdersResponse>(StatusCodes.Status200OK)
				.ProducesProblem(StatusCodes.Status400BadRequest);
		}
	}
}
