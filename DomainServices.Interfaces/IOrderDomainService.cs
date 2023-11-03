using Entities.Models;
using System.Threading.Tasks;

namespace DomainServices.Interfaces
{
    public interface IOrderDomainService
    {
        Task<decimal> GetTotalAsync(Order order, CalculateDeliveryCostAsync calculateDeliveryCostAsync);
    }
}
