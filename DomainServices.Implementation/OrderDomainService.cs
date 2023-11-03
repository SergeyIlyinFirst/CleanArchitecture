using DomainServices.Interfaces;
using Entities.Models;
using System.Linq;
using System.Threading.Tasks;

namespace DomainServices.Implementation
{
    public class OrderDomainService : IOrderDomainService
    {
        public async Task<decimal> GetTotalAsync(Order order, CalculateDeliveryCostAsync calculateDeliveryCostAsync)
        {
            decimal totalPrice = order.Items.Sum(x => x.Quantity * x.Product.Price);
            decimal deliveryCost = 0;
            
            if (totalPrice < 1000)
            {
                decimal totalWeight = order.Items.Sum(x => x.Product.Weight);
                deliveryCost = await calculateDeliveryCostAsync(totalWeight);
            }

            return totalPrice + deliveryCost;
        }
    }
}
