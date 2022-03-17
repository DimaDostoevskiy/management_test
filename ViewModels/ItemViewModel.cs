namespace asu_management.mvc.ViewModels
{
    public class ItemViewModel : BaseViewModel
    {
        public string Name { get; set; }
        public decimal Quantity { get; set; }
        public string Unit { get; set; }
        public int OrderId { get; set; }
    }
}