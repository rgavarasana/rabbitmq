using System;

namespace ravi.learn.rmq.con.console
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Starting RabbitMQ processor");

            var queueConsumer = new RabbitConsumer { Enabled = true };
            queueConsumer.Start();

            Console.WriteLine("Done processing all messages");

            Console.ReadLine();
        }
    }
}
