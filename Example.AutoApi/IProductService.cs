public interface IProductService
{
    // Create (Add) a new product
    void AddProduct(Product? product);

    // Read (Get) all products
    List<Product?> GetAllProducts();

    // Read (Get) a specific product by name
    Product GetProductByName(string productName);

    // Update an existing product's price
    void UpdateProductPrice(string productName, decimal newPrice);

    // Delete a product by name
    void DeleteProduct(string productName);
}