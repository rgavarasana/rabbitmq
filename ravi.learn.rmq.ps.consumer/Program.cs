using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using ravi.learn.rmq.common;
using System;
using ravi.learn.rmq.common.Extensions;
using System.Threading;

namespace ravi.learn.rmq.ps.consumer
{
    class Program
    {
        private static IConnectionFactory _connectionFactory;
        private static IConnection _connection;
        private static string _queueName;
        

        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            ReceiveMessages();
            Console.WriteLine("Closing connections");
            CloseConnections();
            Console.WriteLine("Press ENTER to exit");
        }

        private static void ReceiveMessages()
        {
            _connectionFactory = new ConnectionFactory
            {
                HostName = Constants.RMQ_HOST,
                UserName = Constants.RMQ_USERID,
                Password = Constants.RMQ_PWD
            };
            _connection = _connectionFactory.CreateConnection();

            using (var channel = _connection.CreateModel())
            {
                var consumer = DeclareAndBindToQueue(channel);
                consumer.Received += (sender, e) =>
                {
                    var eventingBasicConsumer = (EventingBasicConsumer)sender;
                    var model = eventingBasicConsumer.Model;
                    var payment = e.Body.Deserialize<Payment>();
                    Console.WriteLine($"Received payment. {payment.ToString()}");
                    Thread.Sleep(500);
                    model.BasicAck(e.DeliveryTag, false);
                };
                channel.BasicConsume(_queueName,false, consumer);
                Console.WriteLine("Receiving messages...");
                Console.ReadLine();
            }
        }

        private static EventingBasicConsumer DeclareAndBindToQueue(IModel channel)
        {
            channel.ExchangeDeclare(Constants.FANOUT_EXCHANGE, "fanout", true, false, null);
            var result = channel.QueueDeclare("", false, true, true, null);
            _queueName = result.QueueName;
            channel.QueueBind(_queueName, Constants.FANOUT_EXCHANGE, "", null);
            return new EventingBasicConsumer(channel);
        }
  
        public static void CreateConnection()
        {
            Console.WriteLine("Creating connections");
            _connectionFactory = new ConnectionFactory
            {
                HostName = Constants.RMQ_HOST,
                UserName = Constants.RMQ_USERID,
                Password = Constants.RMQ_PWD
            };

            _connection = _connectionFactory.CreateConnection();
        }

        public static void CloseConnections()
        {

            _connection?.Dispose();
            _connection = null;
        }
    }
}
