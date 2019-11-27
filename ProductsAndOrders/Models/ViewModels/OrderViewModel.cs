using System.Collections.Generic;

namespace ProductsAndOrders.Models
{
    public class OrderViewModel
    {
        public int OrderId { get; set; }
        public string RecipientName { get; set; }
        public string DestinationCity { get; set; }
        public List<ProductViewModel> Products { get; set; }
    }
}