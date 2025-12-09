using CurrencyConverter.Middleware;
using CurrencyConverter.Services;

var builder = WebApplication.CreateBuilder(args);

// Logging
builder.Logging.ClearProviders();
builder.Logging.AddConsole();

// Services
builder.Services.AddSingleton<ICurrencyService, CurrencyService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Error handling middleware
app.UseErrorHandlingMiddleware();

app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();

app.Run();
