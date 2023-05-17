// See https://aka.ms/new-console-template for more information
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

Console.WriteLine("Welcome to the Tecketing Service");

var factory = new ConnectionFactory()
{
    HostName = "localhost",
    UserName=  "user",
    Password = "mypassword",
    VirtualHost = "/"
};

var connection = factory.CreateConnection();

var channel = connection.CreateModel();

channel.QueueDeclare("bookings", durable: true, exclusive: true);

var consumer = new EventingBasicConsumer(channel);

consumer.Received += (model, eventArgs)=>
{
    // Getting my byte[]
    var body= eventArgs.Body.ToArray();
    var message = Encoding.UTF8.GetString(body);
    Console.WriteLine($"New ticket processing is initiated for - {message}");
};

channel.BasicConsume("bookings",true,consumer);

Console.ReadKey();
