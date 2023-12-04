using BE.Auto.Api.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace Example.AutoApi;

[Authorize]
public class ProductService : IProductService,IAutoApi
{
    private readonly List<Product?> _products = new();

    public ProductService()
    {
        _products = GenerateRandomProducts(10);
    }
    // Create (Add) a new product
    public void AddProduct(Product? product)
    {
        _products.Add(product);
    }
    [AllowAnonymous]
    // Read (Get) all products
    public List<Product?> GetAllProducts()
    {
        return _products;
    }

    // Read (Get) a specific product by name
    public Product GetProductByName(string productName)
    {
        return _products.FirstOrDefault(p => p.Name == productName);
    }

    // Update an existing product's price
    public void UpdateProductPrice(string productName, decimal newPrice)
    {
        var productToUpdate = _products.FirstOrDefault(p => p.Name == productName);

        if (productToUpdate != null)
        {
            productToUpdate.Price = newPrice;
        }
    }

    // Delete a product by name
    public void DeleteProduct(string productName)
    {
        var productToDelete = _products.FirstOrDefault(p => p.Name == productName);

        if (productToDelete != null)
        {
            _products.Remove(productToDelete);
        }
    }
    private static List<Product?> GenerateRandomProducts(int count)
    {
        var random = new Random();
        var productList = new List<Product?>();

        for (var i = 0; i < count; i++)
        {
            var randomProductName = $"Product{i + 1}";
            var randomProductPrice = (decimal)random.Next(10, 1000); // Adjust the range as needed

            var randomProduct = new Product
            {
                Name = randomProductName,
                Price = randomProductPrice
            };

            productList.Add(randomProduct);
        }

        return productList;
    }
}