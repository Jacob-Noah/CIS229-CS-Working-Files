namespace WebApplication1.Models;

public class ProductModel
{
    public int? Id { get; set; }
    public string? Name { get; set; }
    public int? Quantity { get; set; }
    public int? CustomerId { get; set; }

    public ProductModel(int id, string name, int quantity, int customerId)
    {
        Id = id;
        Name = name;
        Quantity = quantity;
        CustomerId = customerId;
    }
}