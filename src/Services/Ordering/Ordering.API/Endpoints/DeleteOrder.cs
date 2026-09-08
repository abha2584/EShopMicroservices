using Ordering.Application.Orders.Commands.DeleteOrder;

namespace Ordering.API.Endpoints
{
	public record DeleteOrderResponse(bool isSucess);
	public class DeleteOrder : ICarterModule
	{
		public void AddRoutes(IEndpointRouteBuilder app)
		{
			app.MapDelete("/orders/{id}", async (Guid Id, ISender sender) =>
			{
				var command = new DeleteOrderCommand(Id);
				var result = await sender.Send(command);
				var response = result.Adapt<DeleteOrderResponse>();
				return Results.Ok(response);
			})
			.WithName("DeleteOrder")
			.WithDescription("Delete Order")
			.WithSummary("Delete Order")
			.Produces<DeleteOrderResponse>(StatusCodes.Status200OK)
			.ProducesProblem(StatusCodes.Status400BadRequest);
		}
	}
}
