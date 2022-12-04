using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

var connectionFactory = new ConnectionFactory()
{
    HostName = "localhost",
};
using var connection = connectionFactory.CreateConnection();
using var channel = connection.CreateModel();
string queueName = "HelloWorld";
channel.QueueDeclare(queueName, false, false, false, null);

var consumer = new EventingBasicConsumer(channel);
consumer.Received += Consumer_Received;

channel.BasicConsume(queueName, true, consumer);
Console.Write("Press [Enter] to exit.");
Console.ReadLine();

void Consumer_Received(object? sender, BasicDeliverEventArgs e)
{
    var body = e.Body.ToArray();
    var stringMessage = Encoding.UTF8.GetString(body);
    Console.WriteLine($"[-] Message received: {stringMessage}");
}