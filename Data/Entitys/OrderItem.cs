namespace asu_management.mvc.Data;

public class OrderItem : BaseEntity
{
    public decimal Quantity { get; set; }
    public string? Unit { get; set; }
    public Order? Order { get; set; }
    public int OrderId { get; set; }

}
