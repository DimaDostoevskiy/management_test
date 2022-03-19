using System.ComponentModel.DataAnnotations;
using asu_management.mvc.Domain;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace asu_management.mvc.ViewModels
{
    public class OrderViewModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Number { get; set; }
        public DateTime Date { get; set; } = DateTime.Now.Date;
        public int ProviderId { get; set; }
        public string ProviderName { get; set; }
        public ItemViewModel[] Items { get; set; }
        public SelectList Providers { get; set; } = OrderRepository.ProvidersList;

    }
}