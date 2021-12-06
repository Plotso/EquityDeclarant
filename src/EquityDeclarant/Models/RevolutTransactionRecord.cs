namespace EquityDeclarant.Models
{
    using System;
    using System.ComponentModel;

    public class RevolutTransactionRecord
    {
        [DisplayName("Date")]
        public DateTime Date { get; set; }
        
        [DisplayName("Ticker")]
        public string Ticker { get; set; }
        
        [DisplayName("Type")]
        public string Type { get; set; }
        
        [DisplayName("Quantity")]
        public decimal? Quantity { get; set; }
        
        [DisplayName("Price per share")]
        public decimal? PricePerShare { get; set; }
        
        [DisplayName("Total amount")]
        public decimal? TotalAmount { get; set; }
        
        [DisplayName("Currency")]
        public string Currency { get; set; }

        [DisplayName("FX Rate")]
        public decimal FXRate { get; set; }
    }
}