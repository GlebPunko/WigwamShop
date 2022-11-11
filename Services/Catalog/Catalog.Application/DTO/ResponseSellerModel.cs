namespace Catalog.Application.DTO
{
    public class ResponseSellerModel
    {
        public int Id { get; set; }
        public string SellerName { get; set; } = null!;
        public string SellerDescription { get; set; } = null!;
        public string SellerPhoneNumber { get; set; } = null!;
    }
}
