namespace Basket.Application.Models
{
    public abstract class BaseOrderModel
    {
        public int SellerId { get; set; }
        public decimal Price { get; set; }
    }
}
