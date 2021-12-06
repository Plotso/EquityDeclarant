namespace EquityDeclarant.Services
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Logging;
    using Models;
    using RestSharp;

    public class BNBRatesProvider : IBNBRaterProvider
    {
        private readonly ILogger<BNBRatesProvider> _logger;

        public BNBRatesProvider(ILogger<BNBRatesProvider> logger)
        {
            _logger = logger;
        }
        public async Task<IEnumerable<CurrencyRate>> GetRates(GetBNBRatesRequest request)
        {
            var url = $@"https://www.bnb.bg/Statistics/StExternalSector/StExchangeRates/StERForeignCurrencies/index.htm?
                downloadOper={request.DownloadOper}&group1={request.Group1}&valutes={request.Valutes}&search={request.Search}&showChart={request.ShowChart}&
                showChartButton={request.ShowChartButton}&type={request.Type}&
                periodStartDays={request.PeriodStartDays}&periodStartMonths={request.PeriodStartMonths}&periodStartYear={request.PeriodStartYear}&
                periodEndDays={request.PeriodEndDays}&periodEndMonths={request.PeriodEndMonths}&periodEndYear={request.PeriodEndMonths}";

            var client = new RestClient(url);
            var restRequest = new RestRequest(Method.GET);
            var response = await client.ExecuteAsync(restRequest);
            
            return ParseRatesInfo(response.Content);
        }

        private IEnumerable<CurrencyRate> ParseRatesInfo(string ratesInfo)
        {
            var result = new List<CurrencyRate>();
            var lines = ratesInfo.Split("\n");
            var validLines = lines.Where(line => !string.IsNullOrEmpty(line) && char.IsDigit(line[0]));
            foreach (var line in validLines)
            {
                var lineData = line.Split(new []{',', ' '}, StringSplitOptions.RemoveEmptyEntries);
                
                var date = DateTime.ParseExact(lineData[0], "dd.MM.yyyy", CultureInfo.InvariantCulture);
                var destinationCurrency = lineData[1];
                var unitCountForRate = int.Parse(lineData[2]);
                var rate = decimal.Parse(lineData[3]);
                result.Add(new CurrencyRate(destinationCurrency, rate, unitCountForRate, date));
            }

            return result;
        }
    }
}