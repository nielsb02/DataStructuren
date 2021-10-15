using System;

namespace Inapkken
{
    class Program
    {
        static void Main(string[] args)
        {
            string line = Console.ReadLine();

            long numberOfBoxes = int.Parse(line.Split(' ')[0]);
            long numberOfPackages = int.Parse(line.Split(' ')[1]);

            long[] Boxes = new long[numberOfBoxes];
            long boxCount = 0;
            for (long i = 0; i < numberOfBoxes; i++)
            {
                Boxes[i] = int.Parse(Console.ReadLine().Split(' ')[0]);
            }
            for (long i = 1; i <= numberOfPackages; i++)
            {
                long package = int.Parse(Console.ReadLine().Split(' ')[0]);
                boxCount += findBox(package, Boxes);
            }
            Console.WriteLine(boxCount);
        }

        public static long findBox(long package, long[] Boxes)
        {
            long i = -1; long j = Boxes.Length + 1;
            while(i < j -1)
            {
                long mid = (i + j) / 2;
                if (Boxes[mid] <= package)
                    i = mid;
                else
                    j = mid;

            }
            return Boxes[j];
        }
    }
}
