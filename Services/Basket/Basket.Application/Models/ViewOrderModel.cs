namespace Basket.Application.Models
{
    public class ViewOrderModel
    {
        public int Id { get; set; }
        public DateTimeOffset CreateDate { get; set; }
        public decimal Price { get; set; }
        public int SellerId { get; set; }
    }
}
