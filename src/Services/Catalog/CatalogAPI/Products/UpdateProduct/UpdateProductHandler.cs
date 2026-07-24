
namespace CatalogAPI.Products.UpdateProduct
{
	public record UpdateProductCommand(Guid Id, string Name, List<string> Category, string Description, string ImageFile, decimal Price) : ICommand<UpdateProductResult>;

	public record UpdateProductResult(Guid Id);

	public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
	{
		public UpdateProductCommandValidator()
		{
			RuleFor(x => x.Id).NotEmpty().WithMessage("Product Id is required.");
			RuleFor(x => x.Name).NotEmpty().WithMessage("Product name is required.");
			RuleFor(x => x.Category).NotEmpty().WithMessage("Product category is required.");
			RuleFor(x => x.Description).NotEmpty().WithMessage("Product description is required.");
			RuleFor(x => x.ImageFile).NotEmpty().WithMessage("Product image file is required.");
			RuleFor(x => x.Price).GreaterThan(0).WithMessage("Product price must be greater than zero.");
		}
	}
	internal class UpdateProductHandler : ICommandHandler<UpdateProductCommand, UpdateProductResult>
	{
		private readonly IDocumentSession _session;
		//private readonly ILogger<UpdateProductHandler> _logger;
		public UpdateProductHandler(IDocumentSession session)
		{
			_session = session;
			//_logger = loger;
		}
		public async Task<UpdateProductResult> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
		{
			//_logger.LogInformation("Handling UpdateProductCommand for ProductId: {ProductId}", command.Id);
			var product = await _session.LoadAsync<Product>(command.Id);
			if (product == null)
			{
				new ProductNotFoundException(command.Id);
			}
			var updatedProduct = new Product
			{
				Id = command.Id,
				Name = command.Name,
				Category = command.Category,
				Description = command.Description,
				ImageFile = command.ImageFile,
				Price = command.Price
			};
			_session.Update(updatedProduct);
			await _session.SaveChangesAsync(cancellationToken);
			return new UpdateProductResult(updatedProduct.Id);

		}
	}
}
