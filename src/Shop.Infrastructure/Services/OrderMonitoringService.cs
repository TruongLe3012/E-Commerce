using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Shop.Infrastructure.Data;

namespace Shop.Infrastructure.Services
{
    public class OrderMonitoringService : BackgroundService
    {
        private readonly IServiceProvider
            _serviceProvider;

        private readonly ILogger<
            OrderMonitoringService> _logger;

        public OrderMonitoringService(
            IServiceProvider serviceProvider,
            ILogger<OrderMonitoringService> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(
            CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation(
                    "Background service running at: {time}",
                    DateTime.UtcNow);

                using var scope =
                    _serviceProvider.CreateScope();

                var context =
                    scope.ServiceProvider
                        .GetRequiredService<AppDbContext>();

                var expiredOrders =
                    await context.Orders
                        .Where(x =>
                            x.Status == "Pending" &&
                            x.CreatedAt <
                            DateTime.UtcNow.AddMinutes(-30))
                        .ToListAsync();

                foreach (var order in expiredOrders)
                {
                    order.Status = "Cancelled";

                    _logger.LogWarning(
                        "Order {OrderId} auto cancelled",
                        order.Id);
                }

                await context.SaveChangesAsync();

                await Task.Delay(
                    TimeSpan.FromSeconds(30),
                    stoppingToken);
            }
        }
    }
}