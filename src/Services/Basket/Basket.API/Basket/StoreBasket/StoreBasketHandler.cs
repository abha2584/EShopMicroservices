


using Discount.Grpc;

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

		private readonly DiscountProtoService.DiscountProtoServiceClient _discountProtoServiceClient;
		public StoreBasketCommandHandler(IBasketRepository basketRepository , DiscountProtoService.DiscountProtoServiceClient discountProtoServiceClient)
		{
			_basketRepository = basketRepository;
			_discountProtoServiceClient = discountProtoServiceClient;
		}
		public async Task<storeBasketResult> Handle(StoreBasketCommand command, CancellationToken cancellationToken)
		{
			//todo: communicate with discount.grpc to get discount for each item in the cart
			foreach(var item in command.cart.Items)
			{
				var discountRequest = new GetDiscountRequest { ProductName = item.ProductName };
				var discountResponse = await _discountProtoServiceClient.GetDiscountAsync(discountRequest , cancellationToken: cancellationToken);
				item.Price -= Convert.ToDecimal(discountResponse.Amount);
			}

			//TODO: store basket
			//TODO : update cache
			await _basketRepository.StoreBasket(command.cart, cancellationToken);
			return new storeBasketResult(command.cart.UserName);
		}
	}
}
