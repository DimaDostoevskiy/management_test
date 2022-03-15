namespace asu_management.mvc.Data
{
    public class Order : BaseEntity
    {
        public string? Number { get; set; }
        public DateTime Date { get; set; }

        public Provider? Provider { get; set; }
        public ICollection<OrderItem>? Items { get; set; }
    }
}