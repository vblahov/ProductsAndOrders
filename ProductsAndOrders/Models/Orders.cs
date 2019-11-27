using System;
using System.Collections.Generic;

namespace ProductsAndOrders.Models
{
    public partial class Orders
    {
        public Orders()
        {
            OrdersProducts = new HashSet<OrdersProducts>();
        }

        public int OrderId { get; set; }
        public string RecipientName { get; set; }
        public string DestinationCity { get; set; }

        public virtual ICollection<OrdersProducts> OrdersProducts { get; set; }
    }
}
