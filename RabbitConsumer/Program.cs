using MassTransit;
using RabbitConsumer;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMassTransit(x =>
{
    x.UsingRabbitMq((context, config) =>
    {
        config.Host(new Uri("rabbitmq://localhost"), h =>
        {
            h.Username("localrabbit");
            h.Password("localrabbit");
        });
        config.ReceiveEndpoint("game", e =>
        {
            e.Consumer<GameConsumer>();
        });
    });
    x.AddConsumer<GameConsumer>();

});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();