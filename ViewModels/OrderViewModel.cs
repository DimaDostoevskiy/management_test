using asu_management.mvc.Domain;
using asu_management.mvc.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace asu_management.mvc.ViewModels
{
    public class OrderViewModel : BaseModel
    {
        public int ProviderId { get; set; }
        public string SortNumber { get; set; }
        public OrderModel[] Orders { get; set; }
        public SelectList Providers { get; set; } = OrderRepository.ProvidersList;
        public DateTime StartSortDate { get; set; } = DateTime.UtcNow.Date.AddMonths(-1);
        public DateTime EndSortDate { get; set; } = DateTime.UtcNow.Date;
    }
}