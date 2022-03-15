namespace asu_management.mvc.Models
{
    public class OrderItemViewModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public decimal Quantity { get; set; }
        public string? Unit { get; set; }
        public int OrderId { get; set; }
    }
}