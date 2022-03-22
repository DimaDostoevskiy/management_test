namespace asu_management.mvc.Data;
public class Provider : BaseEntity
{
    public string Name { get; set; }
    public virtual List<Order> Orders { get; set; } = new();
}