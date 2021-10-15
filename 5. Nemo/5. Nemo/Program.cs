using System;
using System.Collections.Generic;

namespace _5._Nemo
{
    class Program
    {
        static void Main(string[] args)
        {
            string s = Console.ReadLine();
            int species = int.Parse(s.Split(' ')[0]);
            new Ark(species, int.Parse(s.Split(' ')[1]), int.Parse(s.Split(' ')[2]));
        }
    }

    class Ark
    {
        int species, cages, capacity;
        bool end = false;
        int[] weight; 
        int animals = 0, cageCounter = 0, totalWeight = 0;
        Dictionary<int, int> count;

        public Ark(int _species, int _cages, int _capacity)
        {
            species = _species;
            cages = _cages;
            capacity = _capacity;
            weight = new int[species];
            count = new Dictionary<int, int>();


            countArk();
        }

        void countArk()
        {
            List<string> assignemts = new List<string>();
            string str = Console.ReadLine(); 
            for (int t = 0; t < species; t++)
            {
                weight[t] = int.Parse(str.Split(' ')[t]);
            }

            string line;
           
            while (!end)
            {
                line = Console.ReadLine();
                //Assignment(line);
                if (line.Split(' ')[0] != "e")
                    assignemts.Add(line);
                else
                    end = true;
            }
            foreach(string assign in assignemts)
            {
                Assignment(assign);
            }
        }

        void Assignment(string line)
        {
            string assignment = line.Split(' ')[0];
            switch (assignment)
            {   
                case "e":
                    end = true;
                    break;
                case "a":
                    List<int> temp = getList(animals);
                    Console.WriteLine(string.Join(" ", temp));
                    break;
                case "c":
                    int cageNumber;
                    try
                    {
                        cageNumber = count[animals];
                    }
                    catch
                    {
                        cageNumber = 0;
                    }
                    Console.WriteLine("Herhaald " + cageNumber);
                    break;
                case "q":
                    Console.WriteLine("Aantal " + cageCounter + " Gewicht " + totalWeight);
                    break;
                case "p":
                    int i = int.Parse(line.Split(' ')[1]);
                    
                    if ((animals & (1 << i)) == 0 && cageCounter != cages && totalWeight + weight[i] <= capacity)
                    {
                        animals |= 1 << i; 
                        cageCounter++;
                        totalWeight += weight[i];
                    }
                    else
                    {
                        Console.WriteLine("Weiger " + i);
                    }
                    try
                    {
                        count[animals]++;
                    }
                    catch
                    {
                        count.Add(animals, 1);
                    }
                    break;
                case "l":
                    int j = int.Parse(line.Split(' ')[1]);
                    if ((animals & (1 << j)) != 0)
                    {
                        animals = animals & ~(1 << j);
                        cageCounter--;
                        totalWeight -= weight[j];
                        
                        
                    }
                    try
                    {
                        count[animals]++;
                    }
                    catch
                    {
                        count.Add(animals, 1);
                    }
                    break;
                default:
                    break;
            }
        }

        List<int> getList(int _animals)
        {
            List<int> arr = new List<int>();
            int position = 0;
            while (_animals != 0)
            {
                if ((_animals & 1) != 0)
                {
                    arr.Add(position);
                }
                position++;
                _animals = _animals >> 1;
            }
            return arr;
        }

    }
}
