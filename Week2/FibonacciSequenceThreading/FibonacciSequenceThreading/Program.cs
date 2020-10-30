using System;
using System.Threading;


namespace FibonacciSequenceThreading
{
    class Program
    {
        public static int a = 0;
        public static int b = 1;

        public static void Fibonacci()
        {
            
            // In N steps compute Fibonacci sequence iteratively.
            for (int i = 0; i < 10; i++)
            {
                int temp = a;
                a = b;
                b = temp + b;
                Console.WriteLine(a);
            }
        }
        static void Main()
        {
            
            Thread thread1 = new Thread(new ThreadStart(() => Program.Fibonacci()));
            Thread thread2 = new Thread(new ThreadStart(() => Program.Fibonacci()));
            Thread thread3 = new Thread(new ThreadStart(() => Program.Fibonacci()));
            thread1.Start();
            thread2.Start();
            thread3.Start();
            thread1.Join();
            thread2.Join();
            thread3.Join();

            for (int i = 0; i < 30; i++)
            {
                //Console.WriteLine(Fibonacci(i));
            }
        }
    }
}
