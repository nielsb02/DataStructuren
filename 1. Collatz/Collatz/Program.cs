using System;

namespace Collatz
{
    class Program
    {
        //Inside the main void the program read the user input and write the output. The user input is stored inside an array,
        // after recieving all four values we can compute the length for all values, by calling the "calculateCollatz" method. 
        static void Main(string[] args)
        {
            long[] input = new long[4];
            input[0] = long.Parse(Console.ReadLine());
            input[1] = long.Parse(Console.ReadLine());
            input[2] = long.Parse(Console.ReadLine());
            input[3] = long.Parse(Console.ReadLine());
            foreach(long value in input)
            {
                long length = calculateCollatz(value);
                Console.WriteLine(length);
            }
        }

        //This method returns an long called length, which gets increased by 1 every time its has to go trough the while loop,
        // where the value is checked wether its odd or even.   
        static long calculateCollatz(long value)
        {
            long length = 0;

            while(value != 1) // because minimal input = 1, value <= 1 is not necessary.
            {
                length++;
                if (value % 2 == 0)
                    value /= 2;
                else
                    value = value * 3 + 1;
            }
            return length;
        }
    }
}
