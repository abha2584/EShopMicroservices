

using System.Reflection;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BuildingBlocks.Messaging.MassTransit
{
	public static class Extensions
	{

		public static IServiceCollection AddMessageBroker(this IServiceCollection services, IConfiguration configuration,  Assembly? assembly = null)
		{
			services.AddMassTransit(config =>
			{
				config.SetKebabCaseEndpointNameFormatter();
				if(assembly is not null)
				{
					config.AddConsumers(assembly);
				}

				config.UsingRabbitMq((context, cfg) =>
				{
					cfg.Host(new Uri(configuration["MessageBroker:Host"]!), "/", host =>
					{
						host.Username(configuration["MesageBroker:UserName"]);
						host.Password(configuration["MesageBroker:Password"]);
					});
					cfg.ConfigureEndpoints(context);
				});
			});
			return services;
		}
	}
}
