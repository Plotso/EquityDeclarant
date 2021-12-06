namespace EquityDeclarant.Models
{
    using System;

    public record CurrencyRate(string DestinationCurrency, decimal Rate, int UnitCountForRate, DateTime date, string SourceCurrency = "BGN");
}