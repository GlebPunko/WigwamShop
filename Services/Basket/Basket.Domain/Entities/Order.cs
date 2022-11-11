namespace Basket.Domain.Entities
{
    public class Order : BaseEntity
    {
        public DateTimeOffset CreateDate { get; set; }
        public decimal Price { get; set; }
        public int SellerId { get; set; }
        public Seller Seller { get; set; }
    }
}
