namespace asu_management.mvc.Data;

public class Provider
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;

    public IEnumerable<Order> Orders { get; set; } = new List<Order>();
}
