namespace asu_management.mvc.Data
{
    public class Order : BaseEntity
    {
        public string Number { get; set; }
        public DateTime Date { get; set; }
        public virtual ICollection<OrderItem> Items { get; set; } = new List<OrderItem>();
        public Provider Provider { get; set; }
        public int ProviderId  { get; set; }
    }
}