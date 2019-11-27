using System.Collections.Generic;

namespace ProductsAndOrders.Models
{
    public class AddOrderRequest
    {
        public string RecipientName;
        public string DestinationCity;
        public List<AddProductRequest> Products;
    }
}