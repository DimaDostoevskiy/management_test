using asu_management.mvc.Domain;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace asu_management.mvc.ViewModels
{
    public class IndexOrderViewModel
    {
        public OrderViewModel[] Orders { get; set; }
        public string SortNumber { get; set; }
        public int ProviderId { get; set; }
        public SelectList Providers { get; set; } = OrderRepository.ProvidersList;
        public DateTime StartSortDate { get; set; } = DateTime.Now.Date.AddMonths(-1);
        public DateTime EndSortDate { get; set; } = DateTime.Now.Date;
    }
}