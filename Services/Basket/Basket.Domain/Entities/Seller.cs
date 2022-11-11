namespace Basket.Domain.Entities
{
    public class Seller : BaseEntity
    {
        public string Name { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public string Email { get; set; } = null!;
        public List<Order> Orders { get; set; } = null!;
    }
}
