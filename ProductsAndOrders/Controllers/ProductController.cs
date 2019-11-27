using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductsAndOrders.Models;
using ProductsAndOrders.Services;

namespace ProductsAndOrders.Controllers
{
    [Route("api/product")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _service;  
  
        public ProductController(IProductService service)  
        {  
            _service = service;  
        } 
        
        // GET:
        [HttpGet]
        public IActionResult GetAll()  
        {  
            IEnumerable<ProductViewModel> products = _service.GetAll();  
            return Ok(products);  
        }  
        
        //GET api/products/1
        [HttpGet("{id}")]
        public IActionResult FindById(int? id)
        {
            if (id is null)
                return new BadRequestResult();
            var result = _service.GetById(id.Value);
            if (result is null)
                return new NotFoundResult();

            return new OkObjectResult(result);
        }
        
        //GET api/products/search/"IPhone"
        [HttpGet("search/{name}")]
        public IActionResult SearchByName(string name)
        {
            if (String.IsNullOrEmpty(name))
                return new BadRequestResult();
            var result = _service.SearchByName(name);
            if (result is null)
                return new NotFoundResult();
            
            return new OkObjectResult (result);
        }
        
        //POST api/products/1/updatePrice/300
        [HttpPost("{id}/updatePrice/{priceValue}")]
        public IActionResult UpdatePrice(int? id, decimal? priceValue)
        {
            if (id is null || priceValue is null)
                return new BadRequestResult();
            var result = _service.UpdatePrice(id.Value, priceValue.Value);
            if(result is null)
                return new NotFoundResult();
            
            return new OkObjectResult (result);
        }
        
        //PUT api/products/add
        [HttpPut("add")]
        public IActionResult Add([FromBody] AddProductRequest request)
        {
            if (!IsAddProductRequestValid(request))
                return new BadRequestResult();
            var result = _service.Add(request);
            if (!result.Success)
                return new BadRequestObjectResult (result);
            
            return new OkObjectResult (result);
        }

        private bool IsAddProductRequestValid(AddProductRequest request)
        {
            return !(String.IsNullOrEmpty(request.Name)) && !(request.Price is null);
        }
        
        //DELETE api/products/1/delete
        [HttpDelete("{id}/delete")]
        public IActionResult Delete(int? id)
        {
            if (id is null)
                return new BadRequestResult();
            var result = _service.Delete(id.Value);
            if (result is null)
                return new NotFoundResult();
            
            return new OkObjectResult(result);
        }
    }
}