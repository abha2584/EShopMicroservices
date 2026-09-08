
using BuildingBlocks.Pagination;

namespace Ordering.Application.Orders.Queries.GetOrders
{
	public record GetOrdersQuery(PaginationRequest paginationrequest) : IQuery<GetOrdersResult>;

	public record GetOrdersResult(PaginatedResult<OrderDto>Orders);

}
