﻿using System;
using System.Collections.Generic;

namespace ProductsAndOrders.Models
{
    public partial class OrdersProducts
    {
        public int OrdersProductd { get; set; }
        public decimal Price { get; set; }
        public int? ProductId { get; set; }
        public int? OrderId { get; set; }

        public virtual Orders Order { get; set; }
        public virtual Products Product { get; set; }
    }
}
