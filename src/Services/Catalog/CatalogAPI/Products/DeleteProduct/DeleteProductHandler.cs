
using CatalogAPI.Products.UpdateProduct;

namespace CatalogAPI.Products.DeleteProduct
{
	public record DeleteProductCommand(Guid Id) : ICommand<DeleteProductResult>;

	public record DeleteProductResult(bool IsSuccess);

	public class DeleteProductCommandValidator : AbstractValidator<DeleteProductCommand>
	{
		public DeleteProductCommandValidator()
		{
			RuleFor(x => x.Id).NotEmpty().WithMessage("Product Id is required.");
		}
	}
	internal class DeleteProductCommandHandler : ICommandHandler<DeleteProductCommand, DeleteProductResult>
	{
		private readonly IDocumentSession _session;
		//private readonly ILogger<DeleteProductCommandHandler> _logger;
		public DeleteProductCommandHandler(IDocumentSession session)
		{
			_session = session;
			//_logger = loger;
		}
		public async Task<DeleteProductResult> Handle(DeleteProductCommand command, CancellationToken cancellationToken)
		{
            //_logger.LogInformation("Handling DeleteProduct for ProductId: {ProductId}", command.Id);
			// Specify the document type when deleting by id so Marten knows which mapping to use
			_session.Delete<Product>(command.Id);
			await _session.SaveChangesAsync(cancellationToken);
			return new DeleteProductResult(true);

		}
	}

}
