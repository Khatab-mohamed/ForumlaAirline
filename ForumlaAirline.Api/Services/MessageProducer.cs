using System.Text;
using System.Text.Json;
using RabbitMQ.Client;

namespace ForumlaAirline.Api.Services;

public class MessageProducer : IMessageProducer
{
    
    public void SendingMessages<T>(T message)
    {
        
        var factory = new ConnectionFactory()
        {
          HostName = "localhost",
          UserName=  "user",
          Password = "mypassword",
          VirtualHost = "/"
        };

        var connection = factory.CreateConnection();

        var channel = connection.CreateModel();

        channel.QueueDeclare("bookings",durable:true,exclusive:true);

        var jsonString = JsonSerializer.Serialize(message);

        var body = Encoding.UTF8.GetBytes(jsonString);

        channel.BasicPublish("","bookings", body:body);
    }
}