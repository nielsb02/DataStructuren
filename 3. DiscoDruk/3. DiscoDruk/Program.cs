using System;
using System.Collections.Generic;
using System.Linq;

namespace _3._DiscoDruk
{
    class Program
    {
        static void Main(string[] args)
        {
            int totalGuests = int.Parse(Console.ReadLine());
            List<Guest> guests = new List<Guest>();

            //Reads all every line by domJudge, and stores both the time of entering and leaving the disco.
            for (int i = 0; i < totalGuests; i++)
            {
                string s = Console.ReadLine();
                guests.Add(new Guest(true, int.Parse(s.Split(' ')[1])));
                guests.Add(new Guest(false, int.Parse(s.Split(' ')[2])));
            }

            //Sorts the list by time
            //guests = guests.OrderBy(obj => obj.inside).ToList();
            guests = guests.OrderBy(obj => obj.time).ToList();

            int count = 0, highestCount = 0;

            //In the for loop a counter increases or decreases depending on the bool "inside".
            //When the counter hits the highestcounter the time of arrival is stored in a new list called "arrived", the counter can also surpass our highestcounter it will
            //remove all timestamps stored in "arrived" and "left". 
            //If we are on the highest count and a person leaves it will add the "left" timestamp to the list.
            List<Guest> arrived = new List<Guest>();
            List<Guest> left = new List<Guest>();

            for (int index = 0; index < guests.Count(); index++)
            {
                //When a guest leaves at the same time as another one leaves we have to skip this loop, this is checked in the following if statements:
                if(index + 1 != guests.Count())
                {
                    if (guests[index].time == guests[index + 1].time && guests[index].inside != guests[index + 1].inside)// && !guests[index + 1].disable)
                    {
                        index++;
                        continue;
                    }
                }
                if (guests[index].inside)
                {
                    count++;
                    if (count == highestCount)
                    {
                        arrived.Add(new Guest(true, guests[index].time));
                    }
                    else if (count > highestCount)
                    {
                        arrived.Clear();
                        left.Clear();
                        arrived.Add(new Guest(true, guests[index].time));
                        highestCount = count;
                    }
                }
                else
                {
                    if (count == highestCount)
                    {
                        left.Add(new Guest(false, guests[index].time));
                    }
                    count--;
                }
            }
            // Writes the output.
            Console.WriteLine(highestCount);
            for(int i = 0; i < arrived.Count(); i++)
            {
                Console.WriteLine("Van " + arrived[i].time + " tot " + left[i].time);
            }
        }
    }
    

    class comparer<guest>
        {
            public int Compare(string x, string y)
            {
                return string.Compare(x, y, true);
            }
        }

    public class Maths 
    {
        public Random rnd = new Random();

        public void quickSort(List<Guest> guests, int p, int q)
        {
            if (q - p <= 1)
            {
                return;
            }
            if (q - p <= 20)
            {
                //insertionSort(guests, p , q);
                return;
            }
            int t = rnd.Next(p, q);
            int r = split (guests,p,q, t);
            quickSort(guests, p, r);
            quickSort(guests, r + 1, q);
        }

        public int split(List<Guest> guests, int p, int q, int t)
        {
            int r = p, s = q - 1;
            guests = Swap(guests, p, t);
            while (r < s)
            {
                if(guests[r + 1].time <= guests[r].time)
                {
                    guests = Swap(guests, r, r+1);
                    r++;
                }
                else 
                {
                    guests = Swap(guests, s, r+1);
                    s--;
                }
            }
            return r;
        }
        public List<Guest> Swap(List<Guest> list, int indexA, int indexB)
        {
            Guest tmp = list[indexA];
            list[indexA] = list[indexB];
            list[indexB] = tmp;
            return list;
        }
    }

    // Guest stores 3 variables:  inside is a bool which indicates wether a person enters or leaves the disco, 
    // time stores the time in seconds.
    public class Guest
    {
        public Guest(bool type, int time)
        {
            this.inside = type;
            this.time = time;
        }
        public bool inside { set; get; }
        public int time { set; get; }
        public bool disable { set; get; }
    }
}
