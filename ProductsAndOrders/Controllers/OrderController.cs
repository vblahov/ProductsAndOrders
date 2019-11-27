using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using ProductsAndOrders.DataStorage;
using ProductsAndOrders.Models;
using ProductsAndOrders.Tools.Managers;

namespace ProductsAndOrders.Controllers
{
    [Route("api/orders")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderLibraryRepository _libraryRepository;  
  
        public OrderController(IOrderLibraryRepository libraryRepository)  
        {  
            _libraryRepository = libraryRepository;  
        } 
        
        // GET:
        [HttpGet]
        public IActionResult GetAll()  
        {  
            IEnumerable<OrderViewModel> items = _libraryRepository.GetAll();  
            return Ok(items); 
        }  
        
        //GET api/orders/1
        [HttpGet("{id}")]
        public ActionResult<OrderViewModel> FindById(int id)
        {
            return _libraryRepository.GetById(id) != null
                ? new ActionResult<OrderViewModel>(_libraryRepository.GetById(id)) : StatusCode(404);
        }
        
        //GET api/orders/search/"Vlad"
        [HttpGet("search/{recipientName}")]
        public ActionResult<Orders> SearchByRecipientName(string name)
        {
            return _libraryRepository.SearchByRecipientName(name) != null
                ? new ActionResult<Orders>(_libraryRepository.SearchByRecipientName(name)) : StatusCode(404);
        }
        
        //POST api/orders/1/updateDestinationCity/London
        [HttpPost("{id}/updateDestinationCity/{city}")]
        public void UpdateDestinationCity(int id, string city)
        {
            _libraryRepository.UpdateDestinationCity(id, city);
        }
        
        //PUT api/orders/add
        [HttpPut("add")]
        public int Add([FromBody] AddOrderRequest request)
        {
            return _libraryRepository.Add(request);
        }
        
        //DELETE api/orders/1/delete
        [HttpDelete("{id}/delete")]
        public IActionResult Delete(int id)
        {
            return new OkObjectResult(_libraryRepository.Delete(id));
        }
    }
}