


namespace Basket.API.Basket.StoreBasket
{
	public record StoreBasketCommand(ShoppingCart cart) : ICommand<storeBasketResult>;

	public record storeBasketResult(string UserName);

	public class StoreBasketCommnadValidator : AbstractValidator<StoreBasketCommand>
	{
		public StoreBasketCommnadValidator()
		{
			RuleFor(x => x.cart).NotNull().WithMessage("Shopping cart cannot be null.");
			RuleFor(x => x.cart.UserName).NotEmpty().WithMessage("UserName cannot be null.");
		}
	}
	public class StoreBasketCommandHandler : ICommandHandler<StoreBasketCommand, storeBasketResult>
	{
		private readonly IBasketRepository _basketRepository;
		public StoreBasketCommandHandler(IBasketRepository basketRepository)
		{
			_basketRepository = basketRepository;
		}
		public async Task<storeBasketResult> Handle(StoreBasketCommand command, CancellationToken cancellationToken)
		{
			
		await _basketRepository.StoreBasket(command.cart,cancellationToken);
			//TODO: store basket
			//TODO : update cache
			return new storeBasketResult(command.cart.UserName);
		}
	}
}
