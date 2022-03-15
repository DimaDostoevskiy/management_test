namespace asu_management.mvc.Data;

public class Provider : BaseEntity
{
    public ICollection<Order> Orders { get; set; } = new List<Order>();
}