using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PoliceSystem.Api
{
    public class JMSHelper
    {



        public static ConnectionFactory buildConnectionFactory()
        {
            return new ConnectionFactory()
            {
                // HostName = "192.168.99.100",
                //Port = 5672,
                //UserName = "test",
                //Password = "test"

                HostName = "rabbitmq.seclab.marijn.ws",
                Port = 5672,
                UserName = "portugal",
                Password = "s63a"

            };
        }
    }
}