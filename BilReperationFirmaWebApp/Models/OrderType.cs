namespace BilReperationFirmaWebApp.Models
{
    public class OrderType
    {
        public int OrderId { get; set; }
        public int TypeId { get; set; }
        public Order Order { get; set; }
        public Type Type { get; set; }
    }
}
