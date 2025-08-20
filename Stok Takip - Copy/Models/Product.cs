namespace Stok_Takip.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        public string StockCode { get; set; }       
        public string StockName { get; set; }       
        public string? Barcode { get; set; }    
        public int? ShelfNo { get; set; }        
        public string? StockGroup { get; set; }   
        public string? StockType { get; set; }    
        public int? TaxRate { get; set; }          
        public decimal? Price { get; set; }         
    }
}
