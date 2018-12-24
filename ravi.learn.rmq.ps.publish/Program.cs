using System;
using RabbitMQ.Client;
using ravi.learn.rmq.common;

namespace ravi.learn.rmq.ps.publish
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            RabbitMqHelper.CreateConnection();



            RabbitMqHelper.CloseConnections();
            Console.WriteLine("Done processing");
        }
    }

   
}
