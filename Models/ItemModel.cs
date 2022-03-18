namespace asu_management.mvc.Models
{
    public class ItemModel : BaseModel
    {
        public string Name { get; set; }
        public decimal Quantity { get; set; }
        public string Unit { get; set; }
        public int OrderId { get; set; }
    }
}