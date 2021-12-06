namespace EquityDeclarant.Services
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Logging;
    using Models;
    using Models.InputModels;

    public class RevolutStatementParser : IRevolutStatementParser
    {
        private readonly ILogger<RevolutStatementParser> _logger;

        public RevolutStatementParser(ILogger<RevolutStatementParser> logger)
        {
            _logger = logger;
        }

        public async Task<IEnumerable<RevolutTransactionRecord>> GetTransactions(StatementInputModel input)
        {
            using var reader = new StreamReader(input.Statement.OpenReadStream());
            return GetRecords(reader, input.Statement.FileName);
        }
        
        
        private IEnumerable<RevolutTransactionRecord> GetRecords(StreamReader reader, string fileName)
        {
            var fileContent = reader.ReadToEnd();
            if (!string.IsNullOrEmpty(fileContent))
            {
                var lines = fileContent.Split("\n");
                if (lines[0] != "Date,Ticker,Type,Quantity,Price per share,Total Amount,Currency,FX Rate")
                {
                    _logger.LogError($"File '{fileName}' has different headers than the expected one! Headers in file: {lines[0]}");
                    throw new ArgumentException(
                        "Provided CSV has different headers than the expected one! Please verify it's generate by Revolut app.");
                }
                if (lines.Length > 1)
                {
                    var dataLines = lines.Where(l => !string.IsNullOrEmpty(l)).Skip(1).ToArray();
                    return dataLines
                        .Select(line => line.Split(","))
                        .Select(dataInLine =>
                            new RevolutTransactionRecord
                            {
                                Date = ParseDateTime(dataInLine[0]),
                                Ticker = dataInLine[1],
                                Type = dataInLine[2],
                                Quantity = ParseNullableDecimal(dataInLine[3]),
                                PricePerShare = ParseNullableDecimal(dataInLine[4]),
                                TotalAmount = ParseNullableDecimal(dataInLine[5]),
                                Currency = dataInLine[6],
                                FXRate = ParseNullableDecimal(dataInLine[7]) ?? 0m
                            });
                }
            }

            return Array.Empty<RevolutTransactionRecord>();
        }

        private decimal? ParseNullableDecimal(string input) => decimal.TryParse(input, out var value) ? value : (decimal?)null;

        private DateTime ParseDateTime(string input)
        {
            //return DateTime.Parse(input);
            return DateTime.ParseExact(input, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
        }
    }
}