namespace BilReperationFirmaWebApp.Models
{
    public class Type
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public ICollection<OrderType> OrderTypes { get; set; }
    }
}
