namespace asu_management.mvc.Data;
public class OrderItem : BaseEntity
{
    public string Name { get; set; }
    public decimal Quantity { get; set; }
    public string Unit { get; set; }
    public virtual Order Order { get; set; }
}
