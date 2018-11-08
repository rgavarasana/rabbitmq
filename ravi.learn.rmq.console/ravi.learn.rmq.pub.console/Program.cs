using RabbitMQ.Client;
using System;

namespace ravi.learn.rmq.pub.console
{
    class Program
    {       

        static void Main(string[] args)
        {
            Console.WriteLine("Send messages to RabbitMQ");

            try
            {
                Console.WriteLine("Press <<ENTER>> key to send a message. Press <<Q>> to end");

                var messageCount = 0;
                using (var sender = new RabbitSender())
                {
                    while (true)
                    {
                        var consoleKey = Console.ReadKey();
                        if (consoleKey.Key == ConsoleKey.Q)
                        {
                            break;
                        }
                        var message = $"Message: {++messageCount}";
                        Console.WriteLine($"Sending message {message}");

                        sender.SendMessage(message);
                    }
                }
                Console.WriteLine("Done sending messages");

            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }

            Console.ReadLine();

        }

        //private static void CreateQueueNExchange(IModel model)
        //{
        //    model.ExchangeDeclare("MyFirstExchange", ExchangeType.Topic, true, false, null);

        //    model.QueueDeclare("MyFirstQueue", true, false, false, null);

        //    model.QueueBind("MyFirstQueue", "MyFirstExchange", "cars");

        //    Console.WriteLine("Created exchange, queue and binding");
        //}
    }
}
