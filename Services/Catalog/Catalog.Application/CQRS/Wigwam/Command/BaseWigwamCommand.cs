namespace Catalog.Application.CQRS.Wigwam.Command
{
    public class BaseWigwamCommand
    {
        public string WigwamsName { get; set; } = null!;
        public double Height { get; set; }
        public double Width { get; set; }
        public double Weight { get; set; }
        public string Description { get; set; } = null!;
        public decimal Price { get; set; }
        public int SellerInfoId { get; set; }
    }
}
