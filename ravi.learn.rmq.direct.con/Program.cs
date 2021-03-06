﻿using RabbitMQ.Client;
using System;
using ravi.learn.rmq.common;
using RabbitMQ.Client.Events;
using ravi.learn.rmq.common.Extensions;

namespace ravi.learn.rmq.direct.con
{
   

    class Program
    {
        private static IConnectionFactory _connectionFactory;
        private static IConnection _connection;
        private static IModel model;

        static void Main(string[] args)
        {
            Console.WriteLine("Receiving messages");
            ReceiveMessages();
            Console.WriteLine("Closing connections");
            Close();
            Console.WriteLine("Press ENTER to exit");
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

            using (var channel = _connection.CreateModel())
            {
                channel.QueueDeclare(Constants.QUEUE_NAME, true, false, false, null);

                var consumer = new EventingBasicConsumer(channel);
                
                consumer.Received += (sender, e) =>
                {
                    var payment = e.Body.Deserialize<Payment>();
                    Console.WriteLine($"[X] Payment received. Sender: {payment.Name} Amount: {payment.AmountToPay:C} Card: {payment.CardNumber}");
                };
                channel.BasicConsume(Constants.QUEUE_NAME, true, consumer);
                Console.WriteLine("Press enter to terminate");
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
