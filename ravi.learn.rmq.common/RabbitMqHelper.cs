using System;
using System.Collections.Generic;
using System.Text;
using RabbitMQ.Client;

namespace ravi.learn.rmq.common
{
    public static class RabbitMqHelper
    {
        private static IConnectionFactory _connectionFactory;
        private static IConnection _connection;


        public static void CreateConnection()
        {
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

        public void CreateExchange(string exchangeName)
        {

        }
    }
}
