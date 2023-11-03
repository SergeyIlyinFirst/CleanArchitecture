using Delivery.Interfaces;
using System.Threading.Tasks;

namespace Delivery.Company
{
    public class DeliveryService : IDeliveryService
    {
        public async Task<decimal> CalculateDeliveryCostAsync(decimal weight)
        {
            return await Task.FromResult(weight * 10);
        }

        public async Task<bool> IsDeliveredAsync(int orderId)
        {
            return await Task.FromResult(true);
        }
    }
}