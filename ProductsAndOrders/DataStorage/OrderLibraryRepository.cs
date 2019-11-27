using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using ProductsAndOrders.Models;

namespace ProductsAndOrders.DataStorage
{
    public class OrderLibraryRepository : IOrderLibraryRepository
    {
        readonly LibraryContext _libraryContext;
  
        public OrderLibraryRepository(LibraryContext context) 
        {  
            _libraryContext = context;
        }
        
        public IEnumerable<OrderViewModel> GetAll()
        {
            List<OrderViewModel> orders = new List<OrderViewModel>();
            foreach (var order in _libraryContext.Orders.ToList())
            {
                orders.Add(
                    new OrderViewModel { RecipientName = order.RecipientName,
                DestinationCity = order.DestinationCity,
                Products = getProducts(order.OrderId).ToList()
            });
            }
            return orders;
        }

        private IQueryable<ProductViewModel> getProducts(int orderId)
        {
            return (from o in _libraryContext.Orders
                join op in _libraryContext.OrdersProducts on o.OrderId equals op.OrderId
                join p in _libraryContext.Products on op.ProductId equals p.ProductId
                where o.OrderId == orderId
                select new ProductViewModel{Name = p.Name, Price = p.Price});
        }

        public OrderViewModel GetById(int? id)
        {
            var orders = _libraryContext.Orders.FirstOrDefault(order => order.OrderId == id.Value);
            
            return new OrderViewModel
            {
                RecipientName = orders.RecipientName,
                DestinationCity = orders.DestinationCity,
                Products = getProducts(orders.OrderId).ToList()
            };
        }

        public IDeleteMessage Delete(int? id)
        {
//            var orders = _libraryContext.Orders.FirstOrDefault(order => order.OrderId == id);
//            if (orders is null)
//                return StatusCodes.Status404NotFound;
//
//            _libraryContext.Orders.Remove(orders);
//            _libraryContext.SaveChanges();
//            return StatusCodes.Status200OK;
              return null;
        }

        public Orders SearchByRecipientName(string name)
        {
            return _libraryContext.Orders.FirstOrDefault(order => order.RecipientName == name);
        }

        public void UpdateDestinationCity(int id, string city)
        {
            if (GetById(id) is null) return;
            GetById(id).DestinationCity = city;
            _libraryContext.SaveChanges();
        }

        public int Add(AddOrderRequest request)
        {
            var order = new Orders();
            order.RecipientName = request.RecipientName;
            order.DestinationCity = request.DestinationCity;

            var ordersProducts = new List<OrdersProducts>();
            foreach (var product in request.Products)
            {
                var productDB = _libraryContext.Products.FirstOrDefault(prod => prod.Name == product.Name);
                if (productDB is null)
                {
                    return StatusCodes.Status404NotFound;
                }
                ordersProducts.Add(new OrdersProducts {Order = order,
                    Product = productDB,
                    Price = product.Price.Value
                });
            }

            foreach (var item in ordersProducts)
            {
                order.OrdersProducts.Add(item);
            }

            _libraryContext.Orders.Add(order);
            foreach (var item in ordersProducts)
            {
                _libraryContext.OrdersProducts.Add(item);
            }

            _libraryContext.SaveChanges();

            return StatusCodes.Status200OK;
        }
    }
}