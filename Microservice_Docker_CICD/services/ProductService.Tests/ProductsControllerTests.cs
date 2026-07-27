using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductService.Controllers;
using ProductService.Data;
using ProductService.DTOs;
using ProductService.Models;
using Xunit;

namespace ProductService.Tests;

public class ProductsControllerTests
{
    // Fresh in-memory database per test (unique name) so tests don't interfere.
    private static ProductDbContext NewContext()
    {
        var options = new DbContextOptionsBuilder<ProductDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        var db = new ProductDbContext(options);
        db.Database.EnsureCreated(); // applies the 2 seeded products
        return db;
    }

    [Fact]
    public async Task GetAll_ReturnsSeededProducts()
    {
        using var db = NewContext();
        var controller = new ProductsController(db);

        var result = await controller.GetAll();

        var ok = Assert.IsType<OkObjectResult>(result.Result);
        var products = Assert.IsAssignableFrom<IEnumerable<Product>>(ok.Value);
        Assert.Equal(2, products.Count());
    }

    [Fact]
    public async Task Create_AddsANewProduct()
    {
        using var db = NewContext();
        var controller = new ProductsController(db);

        var result = await controller.Create(new ProductInputDto
        {
            Name = "USB-C Hub", Description = "7-in-1", Price = 34.5m, Stock = 12
        });

        var created = Assert.IsType<CreatedAtActionResult>(result.Result);
        var product = Assert.IsType<Product>(created.Value);
        Assert.Equal("USB-C Hub", product.Name);
        Assert.Equal(3, await db.Products.CountAsync()); // 2 seeded + 1 new
    }

    [Fact]
    public async Task Update_ChangesAnExistingProduct()
    {
        using var db = NewContext();
        var controller = new ProductsController(db);

        var result = await controller.Update(1, new ProductInputDto
        {
            Name = "Renamed Mouse", Description = "updated", Price = 25m, Stock = 99
        });

        Assert.IsType<OkObjectResult>(result);
        var updated = await db.Products.FindAsync(1);
        Assert.Equal("Renamed Mouse", updated!.Name);
        Assert.Equal(99, updated.Stock);
    }

    [Fact]
    public async Task Delete_RemovesTheProduct()
    {
        using var db = NewContext();
        var controller = new ProductsController(db);

        var result = await controller.Delete(1);

        Assert.IsType<NoContentResult>(result);
        Assert.Null(await db.Products.FindAsync(1));
        Assert.Equal(1, await db.Products.CountAsync());
    }

    [Fact]
    public async Task GetById_ReturnsNotFound_ForMissingProduct()
    {
        using var db = NewContext();
        var controller = new ProductsController(db);

        var result = await controller.GetById(999);

        Assert.IsType<NotFoundResult>(result.Result);
    }
}
