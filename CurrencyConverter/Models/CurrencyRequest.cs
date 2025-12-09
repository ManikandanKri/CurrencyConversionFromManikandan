namespace CurrencyConverter.Models
{
    public class CurrencyRequest
    {
        public string sourceCurrency { get; set; }
        public string targetCurrency { get; set; }
        public decimal amount { get; set; }
    }
}
