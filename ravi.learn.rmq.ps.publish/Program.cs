using System;
using RabbitMQ.Client;
using ravi.learn.rmq.common;
using ravi.learn.rmq.common.Extensions;

namespace ravi.learn.rmq.ps.publish
{
    class Program
    {
        private static IConnectionFactory _connectionFactory;
        private static IConnection _connection;
        private static IModel _model;

        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            CreateConnection();

            CreateExchange();

            SendMessage(new Payment { Name = "Lisa", AmountToPay = 50.0m, CardNumber = "1234-1234-1234" });
            SendMessage(new Payment { Name = "Smith", AmountToPay = 250.0m, CardNumber = "7363-2324-2121" });
            SendMessage(new Payment { Name = "Reed", AmountToPay = 15.0m, CardNumber = "4544-1234-6676" });
            SendMessage(new Payment { Name = "Wilson", AmountToPay = 1250.0M, CardNumber = "2232-4343-6567" });
            SendMessage(new Payment { Name = "Barnie", AmountToPay = 500.0M, CardNumber = "4522-8786-4546" });
            SendMessage(new Payment { Name = "Rubble", AmountToPay = 1200.0M, CardNumber = "4333-8786-8665" });
            SendMessage(new Payment { Name = "Peter", AmountToPay = 13.0M, CardNumber = "3545-6565-8866" });
            SendMessage(new Payment { Name = "Mater", AmountToPay = 113.0M, CardNumber = "5454-6565-8866" });
            SendMessage(new Payment { Name = "Amith", AmountToPay = 73.0M, CardNumber = "7897-1213-7887" });
            SendMessage(new Payment { Name = "Jack", AmountToPay = 13.0M, CardNumber = "0955-6565-8866" });
            SendMessage(new Payment { Name = "Tony", AmountToPay = 42.0M, CardNumber = "1241-1212-4221" });
            SendMessage(new Payment { Name = "Marshall", AmountToPay = 98.0M, CardNumber = "2322-5666-6766" });

            Console.WriteLine("Closing connections");
            CloseConnections();
            Console.WriteLine("Done processing");
        }


        public static void CreateExchange()
        {
            Console.WriteLine($"Creating fanout exchange {Constants.FANOUT_EXCHANGE}");
            _model.ExchangeDeclare(Constants.FANOUT_EXCHANGE, "fanout", true, false, null);
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
            _model = _connection.CreateModel();
        }

        public static void CloseConnections()
        {
            _model?.Dispose();
            _model = null;
            _connection?.Dispose();
            _connection = null;
        }

        public static void SendMessage(Payment payment)
        {
            _model.BasicPublish(Constants.FANOUT_EXCHANGE, "", null, payment.Serialize());
            Console.WriteLine($"Payment sent {payment.ToString()}");
        }
    }

   
}
