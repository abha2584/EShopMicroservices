using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BuildingBlocks.Exceptions.Handler
{
	public class CustomExceptionHandler(ILogger<CustomExceptionHandler> logger) : IExceptionHandler
	{
		public async ValueTask<bool> TryHandleAsync(HttpContext context, Exception exception, CancellationToken cancellationToken)
		{
			logger.LogError(exception.Message, "An unhandled exception occurred while processing the request.");
			(string Detail, string Title, int statusCode) details = exception switch
			{
				InternalServerException =>
				(
				 exception.Message,
				 exception.GetType().Name,
				 StatusCodes.Status500InternalServerError),
				ValidationException =>
				(
				exception.Message,
					exception.GetType().Name,
					StatusCodes.Status400BadRequest
				),
				BadRequestException =>
				(
					exception.Message,
					exception.GetType().Name,
					StatusCodes.Status400BadRequest
				),
				NotFoundException =>
				(
					exception.Message,
					exception.GetType().Name,
					StatusCodes.Status404NotFound
				),
				_ =>
				(
					exception.Message,
					exception.GetType().Name,
					StatusCodes.Status500InternalServerError
				)
			};

			var problemDetails = new ProblemDetails
			{
				Status = details.statusCode,
				Title = details.Title,
				Detail = details.Detail,
				Instance = context.Request.Path
			};

			problemDetails.Extensions.Add("traceId", context.TraceIdentifier);
			if(exception is ValidationException validationException)
			{
				problemDetails.Extensions.Add("errors", validationException.Errors);
			}

			await context.Response.WriteAsJsonAsync(problemDetails, cancellationToken);
			return true;
		}
	}
}
