using RabbitMQ.Client;
using System;
using System.Text;

namespace ravi.learn.rmq.console
{
    class Program
    {
        private const string RMQ_HOST = "localhost";
        private const string RMQ_USERNAME = "guest";
        private const string RMQ_PWD = "guest";

        static void Main(string[] args)
        {
            Console.WriteLine("Creating RabbitMQ exchanges and queues");

            try
            {
                var connectionFactory = new ConnectionFactory
                {
                    HostName = RMQ_HOST,
                    UserName = RMQ_USERNAME,
                    Password = RMQ_PWD
                };

                var connection = connectionFactory.CreateConnection();
                var model = connection.CreateModel();

                var properties = model.CreateBasicProperties();
                properties.Persistent = false;

                var serializedMessage = Encoding.Default.GetBytes("This is the first message");

                model.BasicPublish("MyFirstExchange", "cars", properties, serializedMessage);

                Console.WriteLine("Published basic message to MyFirstExchange");

               // CreateQueueNExchange(model);


            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }

            Console.ReadLine();

        }

        private static void CreateQueueNExchange(IModel model)
        {
            model.ExchangeDeclare("MyFirstExchange", ExchangeType.Topic, true, false, null);

            model.QueueDeclare("MyFirstQueue", true, false, false, null);

            model.QueueBind("MyFirstQueue", "MyFirstExchange", "cars");

            Console.WriteLine("Created exchange, queue and binding");
        }
    }
}
