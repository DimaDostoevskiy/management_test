namespace asu_management.mvc.Data;

public class OrderItem
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Quantity { get; set; }
    public string Unit { get; set; } = string.Empty;

    public int OrderId { get; set; }
    public Order Order { get; set; } = new Order();
}
