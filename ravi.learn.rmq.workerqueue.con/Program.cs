using RabbitMQ.Client;
using System;
using System.Threading;
using ravi.learn.rmq.common;
using RabbitMQ.Client.Events;
using ravi.learn.rmq.common.Extensions;

namespace ravi.learn.rmq.workerqueue.con
{
    class Program
    {
        private static IConnectionFactory _connectionFactory;
        private static IConnection _connection;
        
        static void Main(string[] args)
        {
            Console.WriteLine("Receiving messages");
            ReceiveMessages();
            Console.WriteLine("Press ENTER to exit");
            Console.WriteLine("Closing connections");
            Close();
            
            Console.ReadLine();
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
            using(var channel = _connection.CreateModel())
            {
                channel.QueueDeclare(Constants.QUEUE_NAME, true, false, false, null);
                channel.BasicQos(0, 2, false);

                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (sender, e) =>
                {
                    var eventingBasicConsumer = (EventingBasicConsumer) sender;
                    var model = eventingBasicConsumer.Model;
                    var payment = e.Body.Deserialize<Payment>();
                    Console.WriteLine($"[X] Payment received. Name: {payment.Name} Card: {payment.CardNumber} Amount: {payment.AmountToPay:C}");
                    Thread.Sleep(1000);
                    model.BasicAck(e.DeliveryTag, false);
                };
                channel.BasicConsume(Constants.QUEUE_NAME, false, consumer);
                Console.WriteLine("Press Enter to exit");
                Console.ReadLine();
            }
        }

        private static void Close()
        {
            _connection?.Dispose();
            _connection = null;

        }
 
    }
}
