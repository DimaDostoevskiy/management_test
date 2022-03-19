using System.ComponentModel.DataAnnotations;

namespace asu_management.mvc.ViewModels
{
    public class ItemViewModel
    {
        public int Id { get; set; }
        
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        [MaxLength(50)]
        public string Unit { get; set; }

        [Required]
        public decimal Quantity { get; set; }
        public int OrderId { get; set; }
        public string OrderNumber { get; set; }
    }
}