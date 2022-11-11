namespace Catalog.Infastructure.Interfaces
{
    public interface IUnitOfWork
    {
        IWigwamRepository Wigwam { get; }
        ISellerRepository Seller { get; }
        Task SaveAsync(CancellationToken cancellationToken); 
    }
}
