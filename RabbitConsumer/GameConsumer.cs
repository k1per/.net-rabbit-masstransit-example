using Common;
using MassTransit;

namespace RabbitConsumer;

public class GameConsumer : IConsumer<Game>
{
    public async Task Consume(ConsumeContext<Game> context)
    {
        var game = context.Message;
        Console.WriteLine($"Received {game.Name} with price {game.Price}");
    }
}