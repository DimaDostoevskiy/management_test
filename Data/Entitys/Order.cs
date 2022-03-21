namespace asu_management.mvc.Data
{
    public class Order : BaseEntity
    {
        public string Number { get; set; }
        public DateTime Date { get; set; }
        public virtual List<OrderItem> Items { get; set; } = new List<OrderItem>();
        public virtual Provider Provider { get; set; }
    }
}