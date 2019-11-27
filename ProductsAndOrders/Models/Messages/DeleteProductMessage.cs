namespace ProductsAndOrders.Models
{
    public class DeleteProductMessage : IDeleteMessage
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Message { get; set; } = "Product successful deleted!";
    }
}