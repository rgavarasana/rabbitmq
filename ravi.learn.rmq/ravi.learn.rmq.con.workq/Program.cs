using System;

namespace ravi.learn.rmq.con.workq
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Ready to send messages. Press <Q> to quit.");
            var publisher = new Publisher();
            try
            {
                while (true)
                {
                    var message = Console.ReadLine();
                    if (message.ToLower() == "Q")
                    {
                        break;
                    }
                    publisher.SendMessage(message);
                    Console.WriteLine($"{message} sent..");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                publisher.Dispose();
            }            

        }
    }
}
