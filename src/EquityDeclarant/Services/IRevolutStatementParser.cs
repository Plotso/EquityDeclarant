namespace EquityDeclarant.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Models;
    using Models.InputModels;

    public interface IRevolutStatementParser
    {
        Task<IEnumerable<RevolutTransactionRecord>> GetTransactions(StatementInputModel input);
    }
}