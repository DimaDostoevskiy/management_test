using asu_management.mvc.Domain;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace asu_management.mvc.ViewModels
{
    public class OrderViewModel : BaseViewModel
    {
        public ProviderViewModel Provider { get; set; }
        public string Number { get; set; } = string.Empty;
        public DateTime Date { get; set; } = DateTime.UtcNow.Date;
        public ICollection<ItemViewModel> Items { get; set; } = new List<ItemViewModel>();
        public ICollection<OrderViewModel> Orders { get; set; } = new List<OrderViewModel>();
        public SelectList Providers { get; set; } = OrderRepository.ProvidersList;
        public DateTime StartSortDate { get; set; } = DateTime.UtcNow.Date.AddMonths(-1);
        public DateTime EndSortDate { get; set; } = DateTime.UtcNow.Date;
    }
}