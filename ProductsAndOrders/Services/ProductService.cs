using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using ProductsAndOrders.DataStorage;
using ProductsAndOrders.Models;

namespace ProductsAndOrders.Services
{
    public class ProductService : ProductLibraryRepository, IProductService
    {
        public ProductService(LibraryContext context) : base(context)
        {
        }
    }
}