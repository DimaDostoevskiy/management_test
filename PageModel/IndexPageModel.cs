using asu_management.mvc.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace asu_management.mvc.PageModel
{
    public class IndexPageModel
    {
        public bool IsSortNumber { get; set; }
        public bool IsSortDay { get; set; }
        public bool IsSortProvider { get; set; }
        public OrderViewModel[] Orders { get; set; }
        public string SortNumber { get; set; }
        public int ProviderId { get; set; }
        public SelectList SelectProviders { get; set; }
        public DateTime StartSortDate { get; set; } = DateTime.Now.Date.AddMonths(-1);
        public DateTime EndSortDate { get; set; } = DateTime.Now.Date.AddDays(1);
    }
}