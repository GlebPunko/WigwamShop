namespace Catalog.Application.CQRS.Seller.Command
{
    public abstract class BaseSellerCommand
    {
        public string SellerName { get; set; } = null!;
        public string SellerDescription { get; set; } = null!;
        public string SellerPhoneNumber { get; set; } = null!;
    }
}
