using System.Collections.Generic;
using System.Threading.Tasks;
using Sclms.DTOS;
using Sclms.UseCases.IServices;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NuGet.ContentModel;
using Xunit; 

public class ProductsTestCases
{
    [Fact]
    public async Task GetAll_Products_ReturnsOkResult()
    {
        // Arrange
        var productServiceMock = new Mock<IProductService>();
        productServiceMock.Setup(service => service.GetAllProductAsync())
                          .ReturnsAsync(new List<ProductDto>());

        var controller = new ProductController(productServiceMock.Object);

        // Act
        var result = await controller.GetProduct();

        // Assert
        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task GetProductLookup_ProductLookup_ReturnsOkResult()
    {
        // Arrange
        var productServiceMock = new Mock<IProductService>();
        productServiceMock.Setup(service => service.GetProductLookup())
                          .ReturnsAsync(new List<ProductLookupDto>());

        var controller = new ProductController(productServiceMock.Object);

        // Act
        var result = await controller.GetProductLookup();

        // Assert
        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task GetProductById_ValidId_ReturnsOkResult()
    {
        // Arrange
        var productId = 1;
        var productServiceMock = new Mock<IProductService>();
        productServiceMock.Setup(service => service.GetProductByIdAsync(productId))
                          .ReturnsAsync(new ProductDto { Id = productId, /* other properties */ });

        var controller = new ProductController(productServiceMock.Object);

        // Act
        var result = await controller.GetProduct(productId);

        // Assert
        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task GetProductById_InvalidId_ReturnsNotFoundResult()
    {
        // Arrange
        var invalidProductId = -1;
        var productServiceMock = new Mock<IProductService>();
        productServiceMock.Setup(service => service.GetProductByIdAsync(invalidProductId))
                          .ReturnsAsync((ProductDto)null);

        var controller = new ProductController(productServiceMock.Object);

        // Act
        var result = await controller.GetProduct(invalidProductId);

        // Assert
        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public async Task AddProduct_ValidProduct_ReturnsCreatedAtActionResult()
    {
        // Arrange
        var validProduct = new ProductDto { /* initialize with valid data */ };
        var productServiceMock = new Mock<IProductService>();
        productServiceMock.Setup(service => service.AddProductAsync(validProduct))
                          .Returns(Task.CompletedTask);

        var controller = new ProductController(productServiceMock.Object);

        // Act
        var result = await controller.AddProduct(validProduct);

        // Assert
        Assert.IsType<CreatedAtActionResult>(result);
    }
}
