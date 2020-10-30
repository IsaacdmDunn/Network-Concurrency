using System;
using System.Threading;

namespace DebuggingWeek3
{
    public class ThreadingStuff
    {
        //sorts array
        public static void Sort(int[] SortNumbers, int count)
        {
            int temp; 
            for (int i = 0; i < 9; i++)
            {
                //smallest = i;
                
                for (int j = i+1; j < 10; j++)
                {
                    if (SortNumbers[i] > SortNumbers[j])
                    {
                        temp = SortNumbers[j];
                        SortNumbers[j] = SortNumbers[i];
                        SortNumbers[i] = temp;
                    }

                }
            }
            //can also use array.sort to be efficient
            //Array.Sort(SortNumbers); 
            Console.WriteLine("sorted" + count);
        }
    }

    class Program
    {
        //generates array
        static void Generate(int[] newNumbers)
        {
            System.Random random = new System.Random();

            for (int i = 0; i < 10; i++)
            {
                newNumbers[i] = random.Next(50);
            }
        }

        //to screen
        static void ToScreen(int[] numToPrint)
        {

            for (int i = 0; i < 10; i++)
            {
                Console.Write(numToPrint[i] + " ");
            }
            Console.WriteLine();
        }

        //main
        static void Main(string[] args)
        {
            //init arrays
            int[] numbers1 = new int[10];
            int[] numbers2 = new int[10];
            int[] numbers3 = new int[10];
            //gen arrays
            Generate(numbers1);
            Generate(numbers2);
            Generate(numbers3);
            //show arrays
            Console.WriteLine("Initial Lists Are: ");
            Console.WriteLine();
            ToScreen(numbers1);
            ToScreen(numbers2);
            ToScreen(numbers3);
            //sort with threads
            Thread thread1 = new Thread(new ThreadStart(() => ThreadingStuff.Sort(numbers1, 1)));
            Thread thread2 = new Thread(new ThreadStart(() => ThreadingStuff.Sort(numbers2, 2)));
            Thread thread3 = new Thread(new ThreadStart(() => ThreadingStuff.Sort(numbers3, 3)));
            thread1.Start();
            thread2.Start();
            thread3.Start();
            thread1.Join();
            thread2.Join();
            thread3.Join();
            //show arrays
            Console.WriteLine("Sorted Lists Are: ");
            Console.WriteLine();
            ToScreen(numbers1);
            ToScreen(numbers2);
            ToScreen(numbers3);
        }
    }
}
