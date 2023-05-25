using System.ComponentModel.DataAnnotations;

namespace BilReperationFirmaWebApp.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Phonenumber { get; set; }

        [Display(Name = "Sign-up Date")]
        public DateTime? SignUpDate { get; set; }
        public ICollection<Order>? Orders { get; set; }

    }
}
