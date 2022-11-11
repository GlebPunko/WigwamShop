namespace Catalog.Application.DTO
{
    public class ResponseWigwamModel
    {
        public int Id { get; set; }
        public string WigwamsName { get; set; } = null!;
        public double Height { get; set; }
        public double Width { get; set; }
        public double Weight { get; set; }
        public string Description { get; set; } = null!;
        public decimal Price { get; set; }
        public int SellerInfoId { get; set; }
    }
}
