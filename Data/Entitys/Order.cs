namespace asu_management.mvc.Data
{
    public class Order : BaseEntity
    {
        public DateTime Date { get; set; }
        public Provider? Provider { get; set; }
        public int ProviderId  { get; set; }
        public ICollection<OrderItem> Items { get; set; } = new List<OrderItem>();
    }
}