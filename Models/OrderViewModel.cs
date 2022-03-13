using System.ComponentModel.DataAnnotations;

namespace asu_management.mvc.Models
{
    public class OrderModel
    {
        public int Id { get; set; }
        public string Number { get; set; } = string.Empty;

        [DisplayFormat(DataFormatString = "{0:yy:MM:dd}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }
        public int ProviderId { get; set; }
    }
}