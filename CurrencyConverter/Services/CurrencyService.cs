using CurrencyConverter.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace CurrencyConverter.Services
{
    public class CurrencyService : ICurrencyService
    {
        private readonly ILogger<CurrencyService> _logger;
        private Dictionary<string, decimal> _rates = new();

        public CurrencyService(IConfiguration config, ILogger<CurrencyService> logger)
        {
            _logger = logger;

             LoadRates();

        // Watch the JSON file for changes
            var watcher = new FileSystemWatcher(".", "exchangeRates.json");
            watcher.NotifyFilter = NotifyFilters.LastWrite;
            watcher.Changed += (_, __) =>
            {
                Thread.Sleep(200); // File lock delay
                LoadRates();
                _logger.LogInformation("Exchange rates refreshed from JSON.");
            };
            watcher.EnableRaisingEvents = true;
        }

         private void LoadRates()
        {
            var json = File.ReadAllText("exchangeRates.json");
            _rates = JsonSerializer.Deserialize<Dictionary<string, decimal>>(json)
                     ?? new Dictionary<string, decimal>();

            _logger.LogInformation("Exchange rates loaded.");
        }
        

        public Task<CurrencyResponse> ConvertAsync(CurrencyRequest request)
        {
            _logger.LogInformation("Converting from {From} to {To}, Amount = {Amount}",
                request.sourceCurrency, request.targetCurrency, request.amount);

            string key = $"{request.sourceCurrency.ToUpper()}_TO_{request.targetCurrency.ToUpper()}";

            if (!_rates.ContainsKey(key))
            {
                _logger.LogWarning("Conversion rate not found for: {Key}", key);
                throw new Exception($"Conversion rate not found for {key}");
            }

            decimal rate = _rates[key];
            decimal chconvertedAmount = request.amount * rate;

            _logger.LogInformation("Rate: {Rate}, Converted Amount: {ConvertedAmount}",
                rate, chconvertedAmount);

            return Task.FromResult(new CurrencyResponse
            {
                sourceCurrency = request.sourceCurrency,
                targetCurrency = request.targetCurrency,
                exchangeRate = rate,
                convertedAmount = chconvertedAmount
            });
        }
    }
}
