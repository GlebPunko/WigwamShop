namespace Catalog.Domain.Entities
{
    public class SellerInfo : BaseModel
    {
        public string SellerName { get; set; } = null!;
        public string SellerDescription { get; set; } = null!;
        public string SellerPhoneNumber { get; set; } = null!;
        public List<WigwamsInfo> Wigwams { get; set; } = null!;
    }
}
