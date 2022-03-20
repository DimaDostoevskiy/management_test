namespace asu_management.mvc.Data;
public class Provider : BaseEntity
{
    public string Name { get; set; }
    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}