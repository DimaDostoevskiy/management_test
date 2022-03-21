using asu_management.mvc.ViewModels;

namespace asu_management.mvc.PageModel
{
    public class OrderDetailsViewModel
    {
        public OrderViewModel Order { get; set; }
        public string SortName { get; set; }
        public string SortUnit { get; set; }
    }
}