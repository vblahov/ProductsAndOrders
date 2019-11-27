using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductsAndOrders.Models;

namespace ProductsAndOrders.DataStorage
{
    public class ProductLibraryRepository: IProductLibraryRepository 
    {  
        readonly LibraryContext _libraryContext;  
  
        public ProductLibraryRepository(LibraryContext context)  
        {  
            _libraryContext = context;
        }  
  
        public IEnumerable<ProductViewModel> GetAll()
        {
            var productsViews = new List<ProductViewModel>();
            foreach (var prod in _libraryContext.Products.ToList())
            {
                productsViews.Add(
                    new ProductViewModel{Name = prod.Name, Price = prod.Price});
            }
            return productsViews;
        }

        public ProductViewModel GetById(int? id)
        {
            var product = _libraryContext.Products.FirstOrDefault(prod => prod.ProductId == id.Value);
            return new ProductViewModel{Name = product.Name, Price = product.Price};
        }

        public ProductViewModel SearchByName(string name)
        {
            var product = _libraryContext.Products.FirstOrDefault(prod => prod.Name == name);
            return new ProductViewModel{Name = product.Name, Price = product.Price};
        }

        public UpdatePriceMessage UpdatePrice(int? id, decimal? price)
        {
            if(!(GetById(id) is null))
            {
                var product = GetById(id);
                var updatePriceMessage =
                    new UpdatePriceMessage
                    {
                        Name = product.Name,
                        OldPrice = product.Price,
                        NewPrice = price.Value
                    };
                GetById(id).Price = price.Value;
                _libraryContext.SaveChanges();
                return updatePriceMessage;
            }

            return null;
        }

        public AddProductMessage Add(AddProductRequest request)
        {
            var product = _libraryContext.Products.FirstOrDefault(prod => prod.Name == request.Name);
            if (!(product is null))
                return new AddProductMessage
                {
                    Name = product.Name,
                    Price = product.Price,
                    Success = false,
                    Message = $"Product with name: {product.Name} already exists!"
                };
            var createdProduct = new Products {Name = request.Name, Price = request.Price.Value};
            _libraryContext.Add(createdProduct);
            _libraryContext.SaveChanges();
            
            return new AddProductMessage
            {
                Name = createdProduct.Name,
                Price = createdProduct.Price,
                Success = true
            };
        }

        public IDeleteMessage Delete(int? id)
        {
            var product = _libraryContext.Products.FirstOrDefault(prod => prod.ProductId == id.Value);
            if (product is null)
                return null;

            var deleteMessage = new DeleteProductMessage
            {
                Name = product.Name,
                Price = product.Price
            };
            _libraryContext.Products.Remove(product);
            _libraryContext.SaveChanges();
            return deleteMessage;
        }
    }  
}