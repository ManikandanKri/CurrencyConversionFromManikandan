1.Currency converter API can be checked from the below url while running app in local
  http://localhost:5076/swagger/index.html

2.It will automatically update the exchange rates if you are changing in exchangerates.json without 
application restart

3.Testcases are added for EUR to INR (Failure test case) and USD to INR(Success test case)

4.Request  format is 

{
  "sourceCurrency": "USD",
  "targetCurrency": "INR",
  "amount": 100
}

5.Response format will be

{
  "sourceCurrency": "USD",
  "targetCurrency": "INR",
  "exchangeRate": 74,
  "convertedAmount": 7400
}
