using CurrencyConverter.Models;
using CurrencyConverter.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;

namespace CurrencyConverter.Tests
{
    public class CurrencyServiceTests
    {
        private readonly CurrencyService _service;

        public CurrencyServiceTests()
        {
            var inMemorySettings = new Dictionary<string, string>
            {
                {"ExchangeRateFilePath", "exchangerates.json"}
            };

            var config = new ConfigurationBuilder()
                        .AddInMemoryCollection(inMemorySettings)
                        .Build();

            // Create test rates file
            File.WriteAllText("TestRates.json", @"{ ""USD_TO_INR"": 74 }");

            var logger = Mock.Of<ILogger<CurrencyService>>();

            _service = new CurrencyService(config, logger);
        }

        [Fact]
        public async Task ConvertAsync_ReturnsCorrectAmount()
        {
            var req = new CurrencyRequest
            {
                sourceCurrency = "USD",
                targetCurrency = "INR",
                amount = 100
            };

            var result = await _service.ConvertAsync(req);

            Assert.Equal(7400, result.convertedAmount);
            Assert.Equal(74, result.exchangeRate);
        }

        [Fact]
        public async Task ConvertAsync_ThrowsException_OnMissingRate()
        {

            var req = new CurrencyRequest
            {
                sourceCurrency = "EUR",
                targetCurrency = "INR",
                amount = 100
            };

             var req2 = new CurrencyRequest
            {
                sourceCurrency = "USD",
                targetCurrency = "INR",
                amount = 100
            };

            var result = await _service.ConvertAsync(req);
            Assert.Equal(7400, result.convertedAmount);

             var result2 = await _service.ConvertAsync(req2);
            Assert.Equal(7400, result2.convertedAmount);

        }
    }
}
