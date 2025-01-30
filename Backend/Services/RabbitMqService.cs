using RabbitMQ.Client;
using System;
using System.Text;

namespace Prescription_and_Doctor_Visit_Management_System.Services
{
    public class RabbitMqService
    {
        private readonly string _hostname = "vhhycwqo:LYLTfATcKzyAqYC6--jyt0lXkqYPT3_J@shark.rmq.cloudamqp.com/vhhycwqo";
        private readonly string _queueName = "medicinesQueue"; // Queue name to send medicines

        public void SendMedicineNamesToQueue(List<string> medicines)
        {
            // Create a new connection factory instance
            var factory = new ConnectionFactory
            {
                HostName = "shark.rmq.cloudamqp.com",
                UserName = "vhhycwqo",
                Password = "LYLTfATcKzyAqYC6--jyt0lXkqYPT3_J",
                VirtualHost = "vhhycwqo"
            };

            // Create a connection and channel using the factory
            using var connection = factory.CreateConnection(); // CreateConnection is a valid method
            using var channel = connection.CreateModel();

            // Declare the queue to send messages to
            channel.QueueDeclare(queue: _queueName,
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

            foreach (var medicine in medicines)
            {
                var body = Encoding.UTF8.GetBytes(medicine);

                // Publish the medicine name to the queue
                channel.BasicPublish(exchange: "",
                                     routingKey: _queueName,
                                     basicProperties: null,
                                     body: body);
                Console.WriteLine($"Sent: {medicine}"); // Output to confirm that the medicine name was sent
            }
        }
    }
}
