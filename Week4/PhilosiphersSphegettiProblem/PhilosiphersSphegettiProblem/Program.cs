using System;
using System.Threading;

namespace PhilosiphersSphegettiProblem
{
    public class Bowl
    {
        private int pasta = 10;
        Random random = new Random();
        Semaphore Forks;

        public Bowl(Semaphore semaphore)
        {
            this.Forks = semaphore;
        }

        public void Think()
        {
            Thread.Sleep(random.Next(501));
        }

        public void Eat(object count) 
        {
            while (pasta > 0)
            {
                if (pasta > 0)
                {
                    Forks.WaitOne();
                    Console.WriteLine("Philosopher {0}  knicks a fork. ", count);
                    pasta--;
                    Console.WriteLine("Philosopher {0}  eats. ", count);
                    Console.WriteLine("Philosopher {0}  puts down fork. ", count);
                }
                Think();
            }
        }
    }

    class Program
    {
        public static Semaphore semaphore;
        public static int value = 5;
        static void Main(string[] args)
        {
            semaphore = new Semaphore(0,2);
            Bowl Spaghetti = new Bowl(semaphore);
            for (int i = 1; i < 5; i++)
            {
                Thread Philosopher = new Thread(new ParameterizedThreadStart(Spaghetti.Eat));
                Philosopher.Start(i);
                Console.WriteLine("Philosopher {0} Sits", i);
            }

            Thread.Sleep(500);
            semaphore.Release(2);
        }
    }
}
