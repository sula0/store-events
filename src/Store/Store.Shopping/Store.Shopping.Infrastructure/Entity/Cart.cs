namespace Store.Shopping.Infrastructure.Entity;

public class Cart
{
    public Dictionary<string, CartEntry> Entries { get; set; }
        
    public decimal Price { get; set; }
}