using DataAccess.Interfaces;
using Delivery.Interfaces;
using Entities.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Mobile.UseCases.Order.BackgroundJobs
{
    public class UpdateDeliveryStatusJob : IJob
    {
        private readonly IDbContext _dbContext;
        private readonly IDeliveryService _deliveryService;

        public UpdateDeliveryStatusJob(IDbContext dbContext, IDeliveryService deliveryService)
        {
            _dbContext = dbContext;
            _deliveryService = deliveryService;
        }

        public async Task ExecuteAsync()
        {
            Console.WriteLine("Job");

            var orders = await _dbContext
                .Orders
                .AsNoTracking()
                .Where(o => o.Status == OrderStatus.Created)
                .ToListAsync();

            var items = orders.Select(o => new { Order = o, Task = _deliveryService.IsDeliveredAsync(o.Id) }).ToList();

            await Task.WhenAll(items.Select(i => i.Task));
            
            items.ForEach(i =>
            {
                if(i.Task.Result)
                {
                    i.Order.Status = OrderStatus.Delivered;
                }
            });

            await _dbContext.SaveChangesAsync();
        }
    }
}
