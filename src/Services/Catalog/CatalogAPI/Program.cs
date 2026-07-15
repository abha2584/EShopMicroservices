var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddCarter();
builder.Services.AddMediatR(config =>
{
	config.RegisterServicesFromAssembly(typeof(Program).Assembly);
});

builder.Services.AddMarten(options =>
{
	options.Connection(builder.Configuration.GetConnectionString("Database")!);
	options.DatabaseSchemaName = "catalog";
}).UseLightweightSessions();


var app = builder.Build();

// Create database schema on startup
//using (var scope = app.Services.CreateScope())
//{
//	var store = scope.ServiceProvider.GetRequiredService<IDocumentStore>();
//	await store.Storage.ApplyAllConfiguredChangesToDatabaseAsync();
//}

// Configure the HTTP request pipeline.

app.MapCarter();
app.Run();
