using System.ComponentModel.DataAnnotations;

namespace BilReperationFirmaWebApp.Models
{
    public class Order
    {
        public int Id { get; set; }
        [Display(Name = "Completed")]
        public bool IsFinished { get; set; }
        [DisplayFormat(DataFormatString = "{0:C2}")]
        public double Price { get; set; }
        public ICollection<OrderType>? OrderTypes { get; set; }
        public int? MechanicId { get; set; }
        public Mechanic? Mechanic { get; set; }
        public int? CustomerId { get; set; }
        public Customer? Customer { get; set; }
    }
}
