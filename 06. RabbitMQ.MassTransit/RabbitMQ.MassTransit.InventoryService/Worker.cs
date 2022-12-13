using InventoryService.Models;
using MassTransit;

namespace RabbitMQ.MassTransit.InventoryService;

public class Worker : BackgroundService
{
    private readonly IBus _bus;

    public Worker(IBus bus)
    {
        _bus = bus;
    }
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            await _bus.Publish(new Order
            {
                OrderId = Guid.NewGuid(),
                Details = "Book",
                Price = 1000,
                UserId = 1

            }, stoppingToken);

            //await Task.Delay(2000);
        }
    }
}
