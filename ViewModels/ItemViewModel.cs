using asu_management.mvc.Domain;
using asu_management.mvc.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace asu_management.mvc.ViewModels
{
    public class ItemViewModel : BaseModel
    {
        public OrderModel Order { get; set; }
        public string SortName { get; set; }
        public string SortUnit { get; set; }
        public string SortQuantity { get; set; }
        public ItemModel[] Items { get; set; }
        public SelectList Providers { get; set; } = OrderRepository.ProvidersList;
    }
}