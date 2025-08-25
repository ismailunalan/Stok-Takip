namespace Stok_Takip.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Code { get; set; } = "";
        public string Name { get; set; } = "";
        public string? Barcode { get; set; }    
        public int? Quantity { get; set; }        
        public string? Group { get; set; }   
        public string? Type { get; set; }    
        public int? TaxRate { get; set; }          
        public decimal? Price { get; set; }         
    }
}
