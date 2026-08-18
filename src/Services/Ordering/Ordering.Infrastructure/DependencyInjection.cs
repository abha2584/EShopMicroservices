using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Ordering.Infrastructure
{
	public static class DependencyInjection
	{
		public static IServiceCollection AddInfrastructureServices(this IServiceCollection services , IConfiguration configuration)
		{
			// Register infrastructure services here
			var connectionstring = configuration.GetConnectionString("Database");
			return services;
		}
	}
}
