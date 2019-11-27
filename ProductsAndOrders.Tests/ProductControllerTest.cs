using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Rewrite.Internal;
using Moq;
using ProductsAndOrders.Controllers;
using ProductsAndOrders.Models;
using ProductsAndOrders.Services;
using Xunit;

namespace ProductsAndOrders.Tests
{
    public class ProductControllerTest
    {
        
        [Fact]
        public void Get_WhenCalled_ReturnsAllItems()
        {
            var mock = new Mock<IProductService>();
            mock.Setup(repo=>repo.GetAll()).Returns(GetTestProducts());
            var controller = new ProductController(mock.Object);
 
            // Act
            var result = controller.GetAll();
            var responseValue = result as OkObjectResult;
            // Assert
            Assert.IsType<OkObjectResult>(responseValue);
            Assert.Equal(GetTestProducts().Count, (responseValue.Value as List<ProductViewModel>).Count());
        }
        
        [Fact]
        public void GetPostById_Return_OkResult()
        {
            //Arrange
            var testId = 1;
            var testProductView = new ProductViewModel{Name = "Mac", Price = 300};
            var mock = new Mock<IProductService>();
            mock.Setup(repo=>repo.GetById(testId)).Returns(testProductView);
            var controller = new ProductController(mock.Object);
 
            // Act
            var result = controller.FindById(testId);

            //Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void GetPostById_Return_NotFoundResult()
        {
            //Arrange
            var testId = 1;
            var mock = new Mock<IProductService>();
            mock.Setup(repo=>repo.GetById(testId)).Returns((ProductViewModel)null);
            var controller = new ProductController(mock.Object);
 
            // Act
            var result = controller.FindById(testId);

            //Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void GetPostById_Return_BadRequestResult()
        {
            //Arrange
            int? testId = null;
            var mock = new Mock<IProductService>();
            mock.Setup(repo=>repo.GetById(testId)).Returns((ProductViewModel)null);
            var controller = new ProductController(mock.Object);
 
            // Act
            var result = controller.FindById(testId);

            //Assert
            Assert.IsType<BadRequestResult>(result);
        }
        
        [Fact]
        public void GetPostByName_Return_OkResult()
        {
            //Arrange
            var testName = "Mac";
            var testProductView = new ProductViewModel{Name = "Mac", Price = 300};
            var mock = new Mock<IProductService>();
            mock.Setup(repo=>repo.SearchByName(testName)).Returns(testProductView);
            var controller = new ProductController(mock.Object);
 
            // Act
            var result = controller.SearchByName(testName);

            //Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void GetPostByName_Return_NotFoundResult()
        {
            //Arrange
            var testName = "batumbs";
            var mock = new Mock<IProductService>();
            mock.Setup(repo=>repo.SearchByName(testName)).Returns((ProductViewModel)null);
            var controller = new ProductController(mock.Object);
 
            // Act
            var result = controller.SearchByName(testName);

            //Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void GetPostByName_Return_BadRequestResult()
        {
            //Arrange
            string testName = null;
            var mock = new Mock<IProductService>();
            mock.Setup(repo=>repo.SearchByName(testName)).Returns((ProductViewModel)null);
            var controller = new ProductController(mock.Object);
 
            // Act
            var result = controller.SearchByName(testName);

            //Assert
            Assert.IsType<BadRequestResult>(result);
        }
        
        [Fact]
        public void UpdatePrice_ValidData_Return_OkResult()
        {
            //Arrange
            var testPrice = 300;
            var testId = 1;
            var testUpdatePriceMessage = 
                new UpdatePriceMessage
                {
                    Name = "Mac",
                    OldPrice = 200,
                    NewPrice = 300
                };
            var mock = new Mock<IProductService>();
            mock.Setup(repo=>repo.UpdatePrice(testId, testPrice))
                .Returns(testUpdatePriceMessage);
            var controller = new ProductController(mock.Object);

            //Act
            var result = controller.UpdatePrice(testId, testPrice) as OkObjectResult;

            //Assert
            Assert.IsType<OkObjectResult>(result);
            Assert.IsAssignableFrom<UpdatePriceMessage>(result.Value);
        }

        [Fact]
        public void UpdatePrice_InvalidData_Return_BadRequest()
        {
            //Arrange
            decimal? testPrice = null;
            int? testId = null;
            var mock = new Mock<IProductService>();
            mock.Setup(repo=>repo.UpdatePrice(testId, testPrice))
                .Returns((UpdatePriceMessage)null);
            var controller = new ProductController(mock.Object);

            //Act
            var result = controller.UpdatePrice(testId, testPrice);

            //Assert
            Assert.IsType<BadRequestResult>(result);
        }
        
        [Fact]
        public void UpdatePrice_ValidData_Return_NotFoundResult()
        {
            //Arrange
            var testPrice = 200;
            var testId = 0;
            var mock = new Mock<IProductService>();
            mock.Setup(repo=>repo.UpdatePrice(testId, testPrice))
                .Returns((UpdatePriceMessage)null);
            var controller = new ProductController(mock.Object);

            //Act
            var result = controller.UpdatePrice(testId, testPrice);

            //Assert
            Assert.IsType<NotFoundResult>(result);
        }
        
        [Fact]
        public void Add_ValidData_Return_OkResult()
        {
            //Arrange
            var request = new AddProductRequest{Name = "Mac", Price = 300};
            var addProductMessage =
                new AddProductMessage
                {
                    Name = "Mac", Price = 300, Success = true
                        
                };
            var mock = new Mock<IProductService>();
            mock.Setup(repo=>repo.Add(request))
                .Returns(addProductMessage);
            var controller = new ProductController(mock.Object);

            //Act
            var result = controller.Add(request) as OkObjectResult;

            //Assert
            Assert.IsType<OkObjectResult>(result);
            Assert.IsAssignableFrom<AddProductMessage>(result.Value);
        }
        
        [Fact]
        public void Add_InvalidData_Return_BadRequestResult()
        {
            //Arrange
            var request = new AddProductRequest{Name = null, Price = 300};
            var mock = new Mock<IProductService>();
            mock.Setup(repo=>repo.Add(request))
                .Returns((AddProductMessage)null);
            var controller = new ProductController(mock.Object);

            //Act
            var result = controller.Add(request);

            //Assert
            Assert.IsType<BadRequestResult>(result);
        }
        
        [Fact]
        public void Add_ValidData_Return_BadRequestResult()
        {
            //Arrange
            var request = new AddProductRequest{Name = "Mac", Price = 300};
            var addProductMessage =
                new AddProductMessage
                {
                    Name = "Mac", Price = 300,
                    Success = false, Message = "Product with name: Mac already exists!"
                };
            var mock = new Mock<IProductService>();
            mock.Setup(repo=>repo.Add(request))
                .Returns(addProductMessage);
            var controller = new ProductController(mock.Object);

            //Act
            var result = controller.Add(request) as BadRequestObjectResult;

            //Assert
            Assert.IsType<BadRequestObjectResult>(result);
            Assert.IsAssignableFrom<AddProductMessage>(result.Value);
        }
        
        [Fact]
        public void Delete_ValidData_Return_OkObjectResult()
        {
            //Arrange
            int? testId = 1;
            var deleteMessage = new DeleteProductMessage
            {
                Name = "Mac",
                Price = 300
            };
            var mock = new Mock<IProductService>();
            mock.Setup(repo=>repo.Delete(testId))
                .Returns(deleteMessage);
            var controller = new ProductController(mock.Object);

            //Act
            var result = controller.Delete(testId) as OkObjectResult;

            //Assert
            Assert.IsType<OkObjectResult>(result);
            Assert.IsAssignableFrom<DeleteProductMessage>(result.Value);
        }
        
        [Fact]
        public void Delete_InvalidData_Return_BadRequestResult()
        {
            //Arrange
            int? testId = null;
            var mock = new Mock<IProductService>();
            mock.Setup(repo=>repo.Delete(testId))
                .Returns((DeleteProductMessage)null);
            var controller = new ProductController(mock.Object);

            //Act
            var result = controller.Delete(testId);

            //Assert
            Assert.IsType<BadRequestResult>(result);
        }
        
        [Fact]
        public void Delete_ValidData_Return_NotFoundResult()
        {
            //Arrange
            int? testId = 0;
            var mock = new Mock<IProductService>();
            mock.Setup(repo=>repo.Delete(testId))
                .Returns((DeleteProductMessage)null);
            var controller = new ProductController(mock.Object);

            //Act
            var result = controller.Delete(testId);

            //Assert
            Assert.IsType<NotFoundResult>(result);
        }
        
        private List<ProductViewModel> GetTestProducts()
        {
            var products = new List<ProductViewModel>
            {
                new ProductViewModel {Name = "Mac", Price = 100},
                new ProductViewModel {Name = "IPhone", Price = 200},
                new ProductViewModel {Name = "Xiomy", Price = 300},
                new ProductViewModel {Name = "Samsung", Price = 400}
            };
            return products;
        }
    }
}