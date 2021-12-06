namespace EquityDeclarant.Models
{
    using System;

    public class GetBNBRatesRequest
    {
        /*
         * params = {
        "downloadOper": "true",
        "group1": "second",
        "valutes": "USD",
        "search": "true",
        "showChart": "false",
        "showChartButton": "false",
        "type": "CSV",
    }

    params["periodStartDays"] = "{:02d}".format(first_date.day)
    params["periodStartMonths"] = "{:02d}".format(first_date.month)
    params["periodStartYear"] = first_date.year

    params["periodEndDays"] = "{:02d}".format(last_date.day)
    params["periodEndMonths"] = "{:02d}".format(last_date.month)
    params["periodEndYear"] = last_date.year
         */

        public GetBNBRatesRequest(DateTime startDate, DateTime endDate, string currency = "USD")
        {
            Valutes = currency;
            PeriodStartDays = startDate.Day;
            PeriodStartMonths = startDate.Month;
            PeriodStartYear = startDate.Year;
            PeriodEndDays = endDate.Day;
            PeriodEndMonths = endDate.Month;
            PeriodEndYear = endDate.Year;
        }

        public bool DownloadOper { get; } = true;
        public string Group1 { get; } = "second";
        public string Valutes { get; set; }  // Currency
        public bool Search { get; } = true;
        public bool ShowChart { get; } = false;
        public bool ShowChartButton { get; } = false;
        public string Type = "CSV";
        public int PeriodStartDays { get; }
        public int PeriodStartMonths { get; }
        public int PeriodStartYear { get; }
        public int PeriodEndDays { get; }
        public int PeriodEndMonths { get; }
        public int PeriodEndYear { get; }
    }
}