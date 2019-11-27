namespace ProductsAndOrders.Models
{
    public class AddProductMessage
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; } = "Product successful created!";
    }
}