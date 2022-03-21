using asu_management.mvc.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace asu_management.mvc.PageModel
{
    public class OrderCreatePageModel
    {
        public OrderViewModel Order { get; set; }
        public SelectList Providers { get; set; }
    }
}