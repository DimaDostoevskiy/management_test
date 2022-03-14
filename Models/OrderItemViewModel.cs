namespace asu_management.mvc.Models
{
    public class OrderItemViewModel
    {
        public string Name { get; set; } = string.Empty;
        public decimal Quantity { get; set; }
        public string Unit { get; set; } = string.Empty;
        public int OrderId { get; set; }
    }
}