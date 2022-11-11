namespace Basket.Application.Interfaces
{
    public interface IPriceControlService
    {
        Task<string> GetPricesSeller(int id , CancellationToken cancellationToken);
        Task<List<string>> GetPricesSellers(CancellationToken cancellationToken);
    }
}
