using InventoryService;
using MassTransit;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


///Our Receiver -------------------------------------
///Configuring MassTransit-RabbitMQ -----------------
builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<OrderConsumer>();

    x.UsingRabbitMq((context, cfg) =>
    {
        //cfg.Host("localhost", "/", h =>
        //{
        //    h.Username("guest");
        //    h.Password("guest");
        //});

        var uri = new Uri(builder.Configuration.GetSection("ServiceBus:Uri").Value);
        cfg.Host(uri, host =>
        {
            host.Username(builder.Configuration.GetSection("ServiceBus:Username").Value);
            host.Password(builder.Configuration.GetSection("ServiceBus:Password").Value);
        });

        ///Exchange:
        cfg.ReceiveEndpoint(builder.Configuration.GetSection("ServiceBus:Queue").Value, c =>
        {
            c.ConfigureConsumer<OrderConsumer>(context);
            //c.configureconsumetopology = false;
        });

        cfg.ConfigureEndpoints(context);
    });
});
//builder.Services.AddHostedService<Worker>();
///--------------------------------------------------


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
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
