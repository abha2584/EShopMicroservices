

namespace Basket.API.Basket.DeleteBasket
{
	public record DeleteBasketCommand(string UserName) : ICommand<DeleteBasketResult>;

	public record DeleteBasketResult(bool IsSuccess);

	public class DeleteBasketCommandValidator : AbstractValidator<DeleteBasketCommand>
	{
		public DeleteBasketCommandValidator()
		{
			RuleFor(x => x.UserName).NotEmpty().WithMessage("UserName cannot be null.");
		}
	}
	public class DeleteBasketCommandHandler : ICommandHandler<DeleteBasketCommand, DeleteBasketResult>
	{
		public readonly IBasketRepository _basketRepository;
		public DeleteBasketCommandHandler(IBasketRepository basketRepository)
		{
			_basketRepository = basketRepository;
		}
		public async Task<DeleteBasketResult> Handle(DeleteBasketCommand command, CancellationToken cancellationToken)
		{
			//TODO: Implement the logic to delete the basket for the given UserName.and update cache
			await _basketRepository.DeleteBasket(command.UserName , cancellationToken);
			return new DeleteBasketResult(IsSuccess: true);
		}
	}
}
