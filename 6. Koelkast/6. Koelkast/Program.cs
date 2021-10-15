using System;
using System.Collections.Generic;
using System.Text;

namespace _6._Koelkast
{
    class Program
    {
        static void Main(string[] args)
        {
            string str = Console.ReadLine();
            new Koelkast(int.Parse(str.Split(' ')[0]), int.Parse(str.Split(' ')[1]), str.Split(' ')[2][0]);
        }
    }

    class Koelkast
    {
        List<string> house = new List<string>();
        int[,] freeSpots;
        uint startStatus;
        int DestinationX, DestinationY;

        public Koelkast(int width, int height, char method)
        {
            freeSpots = new int[width, height];
            for(int i = 0; i < height; i++)
            {
                house.Add(Console.ReadLine());
            }

            startStatus = 0; DestinationX = 0; DestinationY = 0;
            int y = 0;
            foreach(string s in house)
            {
                int x = 0;
                for (int i = 0; i < width; i++) 
                {
                    switch (s[i])
                    {
                        case '!':
                            startStatus += (((uint)x) << 8) + (uint)y;
                            freeSpots[x, y] = 1;
                            break;
                        case '?':
                            freeSpots[x, y] = 1;
                            DestinationX = x;
                            DestinationY = y;
                            break;
                        case '+':
                            startStatus += (((uint)x) << 24) + (((uint)y) << 16);
                            freeSpots[x, y] = 1;
                            break;
                        case '.':
                            freeSpots[x, y] = 1;
                            break;
                        default:
                            freeSpots[x, y] = 0;
                            break;
                    }
                    x++;
                }
                y++;
            }

            if (goalReachable())
            {
                string[] res = solveKoelkast(method);
                Console.WriteLine(res[0]);
                if (method == 'P')
                {
                    Console.WriteLine(res[1]);
                }
            }
            else
            {
                Console.WriteLine("No solution");
            }
        }

        string[] solveKoelkast(char m)
        {
            string[] result = new string[2];
            Dictionary<uint, uint> diction = new Dictionary<uint, uint>();

            Queue<uint> queue = new Queue<uint>();
            diction.Add(startStatus, 0);
            queue.Enqueue(startStatus);

            while (queue.Count != 0)
            {
                uint curr = queue.Peek();

                uint[] location = new uint[4];
                location[0] = (curr >> 24) & 0xff;
                location[1] = (curr >> 16) & 0xff;
                location[2] = (curr >> 8) & 0xff;
                location[3] = curr & 0xff;

                if (DestinationX == location[2] && DestinationY == location[3])
                {
                    bool stop = false;
                    int count = 0;
                    uint last = curr;
                    StringBuilder sb = new StringBuilder();
                    while (!stop)
                    {
                        if (diction[last] == 0)
                            stop = true;
                        else if(m == 'P')
                        {
                            uint temp = diction[last];
                            int dx = (int)((temp >> 24) & 0xff) - (int)((last >> 24) & 0xff);
                            if (dx == -1)
                            {
                                sb.Insert(0, "E");
                            }
                            else if(dx == 1)
                            {
                                sb.Insert(0, "W");
                            }
                            else
                            {
                                int dy = (int)((temp >> 16) & 0xff) - (int)((last >> 16) & 0xff);
                                if (dy == -1)
                                {
                                    sb.Insert(0, "S");
                                }
                                else if (dy == 1)
                                {
                                    sb.Insert(0, "N");
                                }
                            }
                            count++;
                            last = temp;
                        }
                        else
                        {
                            count++;
                            last = diction[last];
                        }
                    }

                    result[0] = count.ToString();
                    result[1] = sb.ToString();
                    queue.Clear();
                    return result;
                }
                queue.Dequeue();
                for (int i = 0; i < 4; i++)
                {
                    string tempDirection = "";
                    int[] intDirection = new int[2];

                    switch (i)
                    {
                        case 0:
                            tempDirection = "N";
                            intDirection[0] = 0;
                            intDirection[1] = -1;
                            break;
                        case 1:
                            tempDirection = "E";
                            intDirection[0] = 1;
                            intDirection[1] = 0;
                            break;
                        case 2:
                            tempDirection = "S";
                            intDirection[0] = 0;
                            intDirection[1] = 1;
                            break;
                        case 3:
                            tempDirection = "W";
                            intDirection[0] = -1;
                            intDirection[1] = 0;
                            break;
                    }

                    if (nextToBox(location, intDirection))
                    {
                        uint push = Push(curr, location, intDirection);
                        if (push != 0)
                        {
                            if (!diction.ContainsKey(push))
                            {                                                             
                                queue.Enqueue(push);
                                diction.Add(push, curr);
                            }
                        }
                    }
                    else
                    {
                        uint step = Step(curr, location, intDirection);
                        if (step != 0)
                        {
                            if (!diction.ContainsKey(step))
                            {
                                queue.Enqueue(step);
                                diction.Add(step, curr);
                            }
                        }
                    }
                }
            }
            result[0] = "No solution";
            return result;
        }

        bool goalReachable()
        {
            for(int i = -1; i <= 1; i+=2)
            {
                if (freeSpots[DestinationX + i, DestinationY] == 1)
                    return true;
            }
            for (int i = -1; i <= 1; i += 2)
            {
                if (freeSpots[DestinationX, DestinationY + i] == 1)
                    return true;
            }
            return false;
        }
        bool nextToBox(uint[] location, int[] dir)
        {
            if (location[0] + dir[0] == location[2] && location[1] + dir[1] == location[3])
            {
                return true;
            }
            return false;
        }
        uint Step(uint curr, uint[] location, int[] dir)
        {
            if(freeSpots[location[0] + dir[0], location[1] + dir[1]] == 1)
                return ((uint)(location[0] + dir[0]) << 24) + ((uint)(location[1] + dir[1]) << 16) + ((uint)(location[2]) << 8) + (uint)(location[3]);
            else
                return 0;
        }
        uint Push(uint curr, uint[] location, int[] dir)
        {
            if (freeSpots[location[2] + dir[0], location[3] + dir[1]] == 1)
            {
                return ((uint)(location[0] + dir[0]) << 24) + ((uint)(location[1] + dir[1]) << 16) + (uint)((location[2] + dir[0]) << 8) + (uint)(location[3] + dir[1]);
            }
            return 0;
        }
    }

 }
