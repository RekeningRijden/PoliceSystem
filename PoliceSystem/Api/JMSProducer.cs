using PoliceSystem.Models.Domain;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Script.Serialization;

namespace PoliceSystem.Api
{
    public class JMSProducer
    {

        private const string QUEUE = "portugal_car_stolen";
        private const string EXCHANGE = "portugal_car_stolen_exchange";
        private const string ROUTING_KEY = "PT";

        public async Task<Boolean> SendCarChange(Car car)
        {
            Theftinfo theft = car.Thefts.Last();
            Address address = car.Stolen ? theft.LastSeenLocation : theft.CarFoundLocation;
            DateTime date = car.Stolen ? theft.LastSeenDate : theft.CarFoundDate;

            Position position = await new LocationCalls().GetPosition(address);

            string json = "{"
                + "\"carIdentifier\": " + Convert.ToString(car.Id) + ","
                + "\"licencePlate\": \"" + car.LicencePlate + "\","
                + "\"stolen\": " + (car.Stolen ? "true" : "false") + ","
                + "\"lastPosition\": {"
                + "\"date\": \"" + date.ToShortDateString() + "\","
                + "\"longitude\": " + position.Longitude.ToString(CultureInfo.InvariantCulture) + ","
                + "\"latitude\": " + position.Latitude.ToString(CultureInfo.InvariantCulture)
                + "}"
                + "}";

           
            
            return await Send(json);
        }

        private async Task<Boolean> Send(string json)
        {
            var factory = JMSHelper.buildConnectionFactory();

            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                var body = Encoding.UTF8.GetBytes(json);

                channel.QueueDeclare(queue: QUEUE, durable: true, exclusive: false, autoDelete: false, arguments: null);
                channel.ExchangeDeclare(exchange: EXCHANGE, type: "direct");
                channel.QueueBind(queue: QUEUE, exchange: EXCHANGE, routingKey: ROUTING_KEY);
                channel.BasicPublish(exchange: EXCHANGE, routingKey: ROUTING_KEY, basicProperties: null, body: body);

                System.Diagnostics.Debug.WriteLine(" [x] Sent {0}", json);
            }

            return true;
        }
    }
}