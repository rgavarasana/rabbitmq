﻿using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Text;

namespace ravi.learn.rmq.con.workq
{
    public class Publisher : IDisposable
    {
        private const string RMQ_HOST = "localhost";
        private const string RMQ_USERID = "guest";
        private const string RMQ_PWD = "guest";
        private const string RMQ_DEFAULT_EXCHANGE = "";
        private const string RMQ_ROUTING_KEY = "MySecondQueue";

        private readonly ConnectionFactory _connectionFactory;
        private readonly IConnection _connection;

        public Publisher()
        {
            _connectionFactory = new ConnectionFactory
            {
                HostName = RMQ_HOST,
                UserName = RMQ_USERID,
                Password = RMQ_PWD
            };
            _connection = _connectionFactory.CreateConnection();
        }

        public  void SendMessage(string message)
        {
            using (var channel = _connection.CreateModel())
            {
                var properties = channel.CreateBasicProperties();
                properties.Persistent = true;

                var serializedMessage = Encoding.UTF8.GetBytes(message);
                channel.BasicPublish(RMQ_DEFAULT_EXCHANGE, RMQ_ROUTING_KEY, properties, serializedMessage);
            }
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                    _connection.Dispose();
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~Publisher() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
}
