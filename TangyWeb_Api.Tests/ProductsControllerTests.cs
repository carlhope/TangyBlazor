using Moq;
using Tangy_Business.Repository.IRepository;
using TangyWeb_API.Controllers;

namespace TangyWeb_Api.Tests
{
    using Moq;
    using NUnit.Framework;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Tangy_DataAccess;
    using Tangy_Models;

    [TestFixture]
    public class ProductControllerTests
    {
        private Mock<IProductRepository> _mockProductRepository;
        private ProductController _controller;

        [SetUp]
        public void Setup()
        {
            _mockProductRepository = new Mock<IProductRepository>();
            _controller = new ProductController(_mockProductRepository.Object);
        }

        [Test]
        public async Task GetAllProducts()
        {
            // Arrange
            _mockProductRepository.Setup(repo => repo.GetAll().Result).Returns(GetAllMockReturnData());

            // Act
            var result = await _controller.GetAll();

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
            //after check is Ok object, treat as Ok object
            var resultAsOK = result as OkObjectResult;
            //assert is List<ProductDto>>
            Assert.IsInstanceOf<List<ProductDTO>>(resultAsOK.Value);
            //get the data from result as List<ProductDTO>
            var returnValue = resultAsOK.Value as List<ProductDTO>;
            //assert counts are equal
            Assert.AreEqual(2, returnValue.Count);
        }

        private List<ProductDTO> GetAllMockReturnData()
        {
            //list needed for fictional return data
            return new List<ProductDTO>
                {
                    new ProductDTO { Id = 1, Name = "Product 1" },
                    new ProductDTO { Id = 2, Name = "Product 2" }
                };
        }
    }

}