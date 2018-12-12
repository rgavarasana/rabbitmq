using System;

namespace ravi.learn.rmq.pub.workq
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var consumer = new Consumer();
            try
            {
                consumer.ProcessMessages();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                consumer.Dispose();
            }

            Console.WriteLine("Done processing messages");
          
            
        }
    }
}
