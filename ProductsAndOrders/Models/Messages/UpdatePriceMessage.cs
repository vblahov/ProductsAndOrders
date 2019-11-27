namespace ProductsAndOrders.Models
{
    public class UpdatePriceMessage
    {
        public string Name { get; set; }
        public decimal OldPrice { get; set; }
        public decimal NewPrice { get; set; }
        public string Message { get; set; } = "Product price was update!";
    }
}