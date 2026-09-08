

namespace BuildingBlocks.Pagination
{
	public class PaginatedResult<TEntity>(int pageIndex, int pageSize, long count, IEnumerable<TEntity> data) where TEntity : class
	{
		public int PageIndex { get; set; } = pageIndex;

		public int PageSize { get; set; } = pageSize;

		public IEnumerable<TEntity> Data { get; } = data;

		public long Count { get; set; } = count;


	}
}
