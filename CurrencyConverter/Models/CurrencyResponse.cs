namespace CurrencyConverter.Models
{
    public class CurrencyResponse
    {
         public string sourceCurrency { get; set; }
        public string targetCurrency { get; set; }
        public decimal exchangeRate { get; set; }
        public decimal convertedAmount { get; set; }
    }
}
