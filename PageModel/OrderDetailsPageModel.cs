using asu_management.mvc.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace asu_management.mvc.PageModel
{
    public class OrderDetailsPageModel
    {
        public OrderViewModel Order { get; set; }
        public string SortName { get; set; }
        public SelectList SortNames { get; set; }
        public string SortUnit { get; set; }
        public SelectList SortUnits { get; set; }
        public decimal StartSortQuantity { get; set; }
        public decimal EndSortQuantity { get; set; }
    }
}