var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, loggerConfig) =>
    loggerConfig
        .MinimumLevel.Information()
        .WriteTo.Console()
);

builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddMemoryCache();
builder.Services.AddHttpClient<IPotterDbBroker, PotterDbBroker>();

builder.Services.AddScoped<ICacheBroker, CacheBroker>();
builder.Services.AddScoped<IBooksService, BooksService>();
builder.Services.AddScoped<ICharactersService, CharactersService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();
app.UseRouting();
app.MapControllers();

app.Run();
