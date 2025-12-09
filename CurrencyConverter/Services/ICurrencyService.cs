using CurrencyConverter.Models;

namespace CurrencyConverter.Services
{
    public interface ICurrencyService
    {
        Task<CurrencyResponse> ConvertAsync(CurrencyRequest request);
    }
}
