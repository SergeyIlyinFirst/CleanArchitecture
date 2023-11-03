using System.Threading.Tasks;

namespace Delivery.Interfaces
{
    public interface IDeliveryService
    {
        Task<decimal> CalculateDeliveryCostAsync(decimal weight);
        Task<bool> IsDeliveredAsync(int orderId);
    }
}
