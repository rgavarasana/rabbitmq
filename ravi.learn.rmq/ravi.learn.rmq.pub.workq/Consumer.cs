using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace ravi.learn.rmq.pub.workq
{
    public class Consumer  : IDisposable
    {
        private const string RMQ_HOST = "localhost";
        private const string RMQ_USERID = "guest";
        private const string RMQ_PWD = "guest";
        private const string RMQ_DEFAULT_EXCHANGE = "";
        private const string RMQ_ROUTING_KEY = "MySecondQueue";

        private readonly ConnectionFactory _connectionFactory;
        private readonly IConnection _connection;

        public Consumer()
        {
            _connectionFactory = new ConnectionFactory
            {
                HostName = RMQ_HOST,
                UserName = RMQ_USERID,
                Password = RMQ_PWD
            };
            _connection = _connectionFactory.CreateConnection();
        }

        public void ProcessMessages()
        {
            using (var channel = _connection.CreateModel())
            {

                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (sender, ea) =>
                {
                    var body = ea.Body;
                    var message = Encoding.UTF8.GetString(body);
                    Console.WriteLine($"[X] Received {message}");
                    var count = message.Split(".").Length - 1;
                    Thread.Sleep(count * 500);
                    Console.WriteLine("Message processed");
                };
                channel.BasicConsume(RMQ_ROUTING_KEY, true, consumer);
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
        // ~Consumer() {
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
