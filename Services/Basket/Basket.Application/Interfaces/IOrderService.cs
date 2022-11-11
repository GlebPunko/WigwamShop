using Basket.Application.Models;

namespace Basket.Application.Interfaces
{
    public interface IOrderService
    {
        Task<List<ViewOrderModel>> GetAllAsync(CancellationToken cancellationToken);
        Task<ViewOrderModel> GetAsync(int id, CancellationToken cancellationToken);
        Task<ViewOrderModel> CreateAsync(CreateOrderModel model, CancellationToken cancellationToken);
        Task DeleteAsync(int id, CancellationToken cancellationToken);
        Task<ViewOrderModel> UpdateAsync(UpdateOrderModel model, CancellationToken cancellationToken);
    }
}
