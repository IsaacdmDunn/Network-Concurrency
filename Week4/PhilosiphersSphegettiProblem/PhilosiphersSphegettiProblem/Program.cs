using System;
using System.Security.Cryptography;
using System.Threading;

namespace PhilosiphersSphegettiProblem
{
    public class Increment
    {
        static public void Add(Semaphore semaphore) 
        {
            semaphore.WaitOne();
            int tempValue = Program.value + 1;
            Program.value = tempValue;

            Console.WriteLine("Previous Value: " + (Program.value - 1));
            Console.WriteLine("Current Value: " + Program.value);

            semaphore.Release();
        }
    }

    class Program
    {
        public static Semaphore semaphore;
        public static int value = 5;
        static void Main(string[] args)
        {
            semaphore = new Semaphore(0,1);
            Thread thread1 = new Thread(new ThreadStart(() => Increment.Add(semaphore)));
            Thread thread2 = new Thread(new ThreadStart(() => Increment.Add(semaphore)));

            thread1.Start();
            thread2.Start();

            Thread.Sleep(500);
            semaphore.Release(1);
        }
    }
}
