namespace WebAPI_V1.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Barcode { get; set; }
        public int ShelfNo { get; set; }
        public string Group { get; set; }
        public string Type { get; set; }
        public int Tax { get; set; }
        public int Price { get; set; }
    }
}
