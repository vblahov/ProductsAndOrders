using System;
using System.Collections.Generic;

namespace ProductsAndOrders.Models
{
    public partial class Products
    {
        public Products()
        {
            OrdersProducts = new HashSet<OrdersProducts>();
        }

        public int ProductId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }

        public virtual ICollection<OrdersProducts> OrdersProducts { get; set; }
    }
}
