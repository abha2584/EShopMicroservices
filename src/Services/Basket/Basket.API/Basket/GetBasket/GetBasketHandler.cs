

namespace Basket.API.Basket.GetBasket
{
	public record GetBasketQuery(string UserName) : IQuery<GetBasketResult>;
	public record GetBasketResult(ShoppingCart Cart);
	internal class GetBasketQueryHandler : IQueryHandler<GetBasketQuery, GetBasketResult>
	{
		private readonly IBasketRepository _basketRepository;
		public GetBasketQueryHandler(IBasketRepository basketRepository)
		{
			_basketRepository = basketRepository;
		}
		public async Task<GetBasketResult> Handle(GetBasketQuery query, CancellationToken cancellationToken)
		{
			var basket = await _basketRepository.GetBasket(query.UserName);
			return new GetBasketResult(basket);
		}
	}
}
