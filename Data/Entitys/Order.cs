namespace asu_management.mvc.Data
{
    public class Order
    {
        public int Id { get; set; }
        public string Number { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public int ProviderId { get; set; }
    }
}