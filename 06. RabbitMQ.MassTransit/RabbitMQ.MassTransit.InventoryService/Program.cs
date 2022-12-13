using InventoryService;
using MassTransit;
using RabbitMQ.MassTransit.InventoryService;

var builder = WebApplication.CreateBuilder(args);

///Our Receiver -------------------------------------
///Configuring MassTransit-RabbitMQ -----------------
builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<OrderConsumer>();

    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host("localhost", "/", h =>
        {
            h.Username("guest");
            h.Password("guest");
        });

        ///Exchange:
        cfg.ReceiveEndpoint(builder.Configuration.GetSection("ServiceBus:Queue").Value, c =>
        {
            c.ConfigureConsumer<OrderConsumer>(context);
        });

        cfg.ConfigureEndpoints(context);
    });
});
//builder.Services.AddHostedService<Worker>();
///--------------------------------------------------


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
