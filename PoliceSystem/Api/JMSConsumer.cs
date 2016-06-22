using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Web;

namespace PoliceSystem.Api
{
    public class JMSConsumer
    {

        private const string QUEUE = "portugal_foreign_car_stolen";
        private const string EXCHANGE = "portugal_foreign_car_stolen_exchange";
        private const string ROUTING_KEY = "PT";

        public void init()
        {
            Thread listener = new Thread(() =>
            {
                JMSHandler handler = new JMSHandler();

                var factory = JMSHelper.buildConnectionFactory();
                using (var connection = factory.CreateConnection())
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(queue: QUEUE, durable: true, exclusive: false, autoDelete: false, arguments: null);
                    channel.ExchangeDeclare(exchange: EXCHANGE, type: "direct");
                    channel.QueueBind(queue: QUEUE, exchange: EXCHANGE, routingKey: ROUTING_KEY);

                    var consumer = new EventingBasicConsumer(channel);
                    consumer.Received += (model, ea) =>
                    {
                        var message = Encoding.UTF8.GetString(ea.Body);
                        handler.handle(message);
                    };

                    channel.BasicConsume(queue: QUEUE, noAck: true, consumer: consumer);

                    while (true) ;
                }
            });

            listener.Start();
        }
    }
}