using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;

namespace ravi.learn.rmq.con.console
{
    public class RabbitConsumer : IDisposable
    {
        private const string RMQ_HOST = "localhost";
        private const string RMQ_USERNAME = "guest";
        private const string RMQ_PWD = "guest";
        private const string RMQ_DEFAULT_EXCHANGE = "";
        private const string RMQ_QUEUE = "FirstQueue";

        private readonly ConnectionFactory _connectionFactory;
        private readonly IConnection _connection;
        
        public RabbitConsumer()
        {
            _connectionFactory = new ConnectionFactory
            {
                HostName = RMQ_HOST,
                UserName = RMQ_USERNAME,
                Password = RMQ_PWD
            };
            _connection = _connectionFactory.CreateConnection();
        }

        public void Start()
        {
            using (var channel = _connection.CreateModel())
            {
                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (sender, e) =>
                {
                    var body = e.Body;
                    var message = Encoding.UTF8.GetString(body);
                    Console.WriteLine($" [x] Received {message}");
                };
                channel.BasicConsume(RMQ_QUEUE, true, consumer);
                Console.WriteLine("Press <ENTER> to exit");
                Console.ReadLine();
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
        // ~RabbitConsumer() {
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
