namespace Ordering.API
{
	public static class DependencyInjection
	{
		public static IServiceCollection AddApiServices(this IServiceCollection services)
		{
			// Register API services here
			return services;
		}

		public static WebApplication UseApiServices(this WebApplication app)
		{
			// Configure API services here
			return app;
		}
	}
}
