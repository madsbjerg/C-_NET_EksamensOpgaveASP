using System.ComponentModel.DataAnnotations;

namespace BilReperationFirmaWebApp.Models
{
    public class Mechanic
    {
        public int Id { get; set; }
        public string Name { get; set; }

        [Display(Name="Hiring Date")]
        [DataType(DataType.Date)]
        public DateTime HiringDate { get; set; }
        [DisplayFormat(DataFormatString = "{0:C2}")]
        public double Salary { get; set; }
        public ICollection<Order>? Orders { get; set; }
    }
}
