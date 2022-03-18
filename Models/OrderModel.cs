namespace asu_management.mvc.Models
{
    public class OrderModel : BaseModel
    {
        public string Number { get; set; } = string.Empty;
        public DateTime Date { get; set; } = DateTime.UtcNow.Date;
        public int ProviderId { get; set; }
        public string ProviderName { get; set; }
        public ItemModel[] Items { get; set; }
    }
}