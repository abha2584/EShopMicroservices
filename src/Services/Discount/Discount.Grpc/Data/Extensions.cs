using Microsoft.EntityFrameworkCore;

namespace Discount.Grpc.Data
{
	public static class Extensions
	{
        public static IApplicationBuilder UseMigration(this IApplicationBuilder app)
		{
			using (var serviceScope = app.ApplicationServices.CreateScope())
			{
				var serviceProvider = serviceScope.ServiceProvider;
				// Ensure the context is available and run migrations synchronously so failures surface immediately
				var context = serviceProvider.GetRequiredService<DiscountContext>();
				try
				{
					context.Database.Migrate();
				}
				catch (Exception)
				{
					// Let the exception bubble up so the host fails fast and the error is visible in logs
					throw;
				}
			}
			return app;
		}

	}
}
