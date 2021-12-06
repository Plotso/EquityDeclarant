namespace EquityDeclarant.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Models;

    public interface IBNBRaterProvider
    {
        Task<IEnumerable<CurrencyRate>> GetRates(GetBNBRatesRequest request);
    }
}