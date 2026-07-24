using System.Diagnostics;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BuildingBlocks.Behaviours
{
	public class LoggingBehaviour<Trequest, TResponse>(ILogger<LoggingBehaviour<Trequest, TResponse>> logger) : IPipelineBehavior<Trequest, TResponse>
		where Trequest : notnull, IRequest<TResponse>
		where TResponse : notnull
	{
		public async Task<TResponse> Handle(Trequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
		{
			logger.LogInformation("[START] Handling {RequestName} with content: {@Request}", typeof(Trequest).Name, request);

			var timer = new Stopwatch();
			timer.Start();

			var response = await next();

			timer.Stop();
			
			var timetaken = timer.Elapsed;
			if ( timetaken.Seconds > 3)
			{
				logger.LogWarning("[PERFORMANCE] The request  {Request} took {timetaken}" , typeof(Trequest).Name , timetaken.Seconds);
			}

			logger.LogInformation("[END] Handled {Request} with {Response}", typeof(Trequest).Name, typeof(TResponse).Name);
			return response;
		}
	}
}
