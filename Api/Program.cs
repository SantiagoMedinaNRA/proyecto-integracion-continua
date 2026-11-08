var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, loggerConfig) =>
    loggerConfig
        .MinimumLevel.Information()
        .WriteTo.Console()
);

builder.Services.AddControllers();
builder.Services.AddOpenApi();

// Add distributed Redis cache (Valkey compatible)
var valkeyConnection = builder.Configuration["ValkeyConnection"] ?? "localhost:6379";
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = valkeyConnection;
});

// Add health checks
builder.Services.AddHealthChecks()
    .AddRedis(valkeyConnection, name: "valkey", tags: new[] { "ready" });

builder.Services.AddHttpClient<IPotterDbBroker, PotterDbBroker>();
builder.Services.AddCors(options => options.AddDefaultPolicy(policy => policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()));

builder.Services.AddScoped<ICacheBroker, CacheBroker>();
builder.Services.AddScoped<IBooksService, BooksService>();
builder.Services.AddScoped<ICharactersService, CharactersService>();
builder.Services.AddScoped<ISpellsService, SpellsService>();
builder.Services.AddScoped<IPotionsService, PotionsService>();
builder.Services.AddScoped<IMoviesService, MoviesService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
  app.MapOpenApi();
  app.UseDeveloperExceptionPage();
}

app.UseCors();

// Map health check endpoint BEFORE UseRouting
app.MapGet("/health", async (context) =>
{
    context.Response.ContentType = "text/plain";
    await context.Response.WriteAsync("Healthy");
});

app.MapControllers();

await app.RunAsync();