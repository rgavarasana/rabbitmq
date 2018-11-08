using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Text;

namespace ravi.learn.rmq.pub.console
{   
    public class RabbitSender : IDisposable
    {
        private const string RMQ_HOST = "localhost";
        private const string RMQ_USERNAME = "guest";
        private const string RMQ_PWD = "guest";
        private const string RMQ_DEFAULT_EXCHANGE = "";

        private readonly ConnectionFactory _connectionFactory;
        private readonly IConnection _connection;
        private readonly IModel _model;

        public RabbitSender()
        {
            _connectionFactory = new ConnectionFactory
            {
                HostName = RMQ_HOST,
                UserName = RMQ_USERNAME,
                Password = RMQ_PWD
            };

            _connection = _connectionFactory.CreateConnection();
            _model = _connection.CreateModel();
        }

        public void SendMessage(string message)
        {     
            var properties = _model.CreateBasicProperties();
            properties.Persistent = true;

            var serializedMessage = Encoding.Default.GetBytes(message);

            _model.BasicPublish(RMQ_DEFAULT_EXCHANGE, "", properties, serializedMessage);
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _connection.Dispose();                    
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~RabbitSender() {
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
