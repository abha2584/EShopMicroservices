using MediatR;
using BuildingBlocks.CQRS;

using FluentValidation;

namespace BuildingBlocks.Behaviours
{
	public class ValidationBehaviour<TRequest, TResponse>(IEnumerable<IValidator<TRequest>> validators) : IPipelineBehavior<TRequest, TResponse>
		where TRequest : ICommand<TResponse>
	{
		public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
		{
			var context = new ValidationContext<TRequest>(request);
			var validatonResults = await Task.WhenAll(validators.Select(v => v.ValidateAsync(context, cancellationToken)));
			var failures = validatonResults.SelectMany(r => r.Errors).Where(f => f != null).ToList();

			if (failures.Any())
			{
				throw new ValidationException(failures);
			}

			return await next();
		}
	}
}
