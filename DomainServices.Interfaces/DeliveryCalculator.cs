using System.Threading.Tasks;

namespace DomainServices.Interfaces
{
    public delegate Task<decimal> CalculateDeliveryCostAsync(decimal weight);
}
