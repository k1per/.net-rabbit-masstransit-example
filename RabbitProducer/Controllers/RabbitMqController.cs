using Common;
using MassTransit;
using Microsoft.AspNetCore.Mvc;

namespace RabbitProducer.Controllers;
[Route("/api/[controller]")]
[ApiController]
public class RabbitMqController : ControllerBase
{
    private IBus _bus;
    //send message to rabbitmq
    public RabbitMqController(IBus bus)
    {
        _bus = bus;
    }

    [HttpPost("sendGameMEssage")]
    public async Task<IActionResult> SendGameMessage()
    {
        var game = new Game { Name = "Elden Ring", Price = 100 };

        var url = new Uri("rabbitmq://localhost/game");
        var endpoint = await _bus.GetSendEndpoint(url);
        await endpoint.Send(game);

        return Ok("Game message send");
    }
}