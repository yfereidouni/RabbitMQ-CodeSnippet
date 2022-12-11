using InventoryService.Models;
using MassTransit;

namespace InventoryService;

public class OrderConsumer : IConsumer<Order>
{
    public async Task Consume(ConsumeContext<Order> context)
    {
        var msg = context.Message;

        // TO DO : save order to database and ....

        await Console.Out.WriteLineAsync(msg.Details);
    }
}
 