
using BuildingBlocks.Exceptions.Handler;
using Discount.Grpc;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCarter();
builder.Services.AddMediatR(cfg =>
{
	cfg.RegisterServicesFromAssembly(typeof(Program).Assembly);
	cfg.AddOpenBehavior(typeof(LoggingBehaviour<,>));
	cfg.AddOpenBehavior(typeof(ValidationBehaviour<,>));
});

builder.Services.AddMarten(options =>
{
	options.Connection(builder.Configuration.GetConnectionString("Database")!);
	options.Schema.For<ShoppingCart>().Identity(x => x.UserName);
}).UseIdentitySessions();


builder.Services.AddScoped<IBasketRepository, BasketRepository>();
builder.Services.Decorate<IBasketRepository, CachedBasketRepository>();
builder.Services.AddStackExchangeRedisCache(options =>
{
	options.Configuration = builder.Configuration.GetConnectionString("Redis")!;
});
//alternate way without using Scrutor
//builder.Services.AddScoped<IBasketRepository>(provider =>
//{
//	var basketRepository = provider.GetRequiredService<BasketRepository>();
//	var cache = provider.GetRequiredService<IDistributedCache>();
//	return new CachedBasketRepository(basketRepository, cache);
//});


builder.Services.AddGrpcClient<DiscountProtoService.DiscountProtoServiceClient>(options =>
{
	options.Address = new Uri(builder.Configuration["GrpcSettings:DiscountUrl"]!);
})
.ConfigurePrimaryHttpMessageHandler(() =>
{
	var handler = new HttpClientHandler
	{
		ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
	};

	return handler;
});
builder.Services.AddExceptionHandler<CustomExceptionHandler>();

builder.Services.AddHealthChecks()
	.AddNpgSql(builder.Configuration.GetConnectionString("Database")!)
	.AddRedis(builder.Configuration.GetConnectionString("Redis")!);



var app = builder.Build();

app.MapCarter();
app.UseExceptionHandler(options =>{});
app.UseHealthChecks("/health" ,
	new HealthCheckOptions
	{
		ResponseWriter =UIResponseWriter.WriteHealthCheckUIResponse
	});

app.Run();
