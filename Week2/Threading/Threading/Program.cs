using System;
using System.Threading;
using System.Transactions;

namespace Threading
{
    class Program
    {
        public static int number = 0;
        public static int threadCount = 1;

        public static void DoWork(object _i)
        {
            if (Convert.ToInt32(_i) < 500)
            {
                for (int i = Convert.ToInt32(_i); i <= Convert.ToInt32(_i) + 10; i++)
                {
                    Console.WriteLine(i);
                }
                Thread.Sleep(100);
                number += 10;

                _i = number;

                Thread thread1 = new Thread(DoWork);
                Thread thread2 = new Thread(DoWork);
                thread1.Start(_i);
                thread2.Start(_i);
                threadCount += 2;
                Console.WriteLine("threadCount:------------" + threadCount);
            }
        }
    }

    class ThreadTest
    {
        public static void Main()
        {
            int i = 0;
            Thread threads = new Thread(Program.DoWork);
            threads.Start(i);
        }
    }
}


//namespace Threading
//{
//    class Program
//    {
//        public static int threadCount = 1;

//        public static void DoWork(int _i)
//        {
//            if (Program.threadCount < 250)
//            {


//                for (int i = _i; i < _i + 10; i++)
//                {
//                    Console.WriteLine(i);
//                    Thread.Sleep(100);
//                }
//                _i += 10;

//                Thread thread1 = new Thread(() => Program.DoWork(_i));
//                Thread thread2 = new Thread(() => Program.DoWork(_i));
//                thread1.Start();
//                thread2.Start();
//                threadCount += 2;
//                Console.WriteLine("threadCount:------------" + threadCount);
//            }
//        }
//    }

//    class ThreadTest
//    {
//        public static void Main()
//        {
//            int i = 0;
//            Thread threads = new Thread(() => Program.DoWork(i));
//            threads.Start();
//        }

//    }
//}