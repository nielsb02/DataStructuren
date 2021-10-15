using System;
using System.Collections.Generic;

namespace _4._MotorMerge
{
    class Program
    {
        static void Main(string[] args)
        {
            string s = Console.ReadLine();
            int n = int.Parse(s.Split(' ')[0]);
            new MotorMerge(n);
        }
    }

    public class MotorMerge
    {
        public List<Motor> list1 = new List<Motor>();
        public List<Motor> list2 = new List<Motor>();
        public MotorMerge(int amount)
        {
            for(int t=0; t<amount; t++)
            {
                string s = Console.ReadLine();
                Motor m = new Motor(s.Split(' ')[0], int.Parse(s.Split(' ')[1]), int.Parse(s.Split(' ')[2]), int.Parse(s.Split(' ')[3]));
                list1.Add(m);
                list2.Add(m);
            }

            mergeSort(list1, 0, amount - 1, 0);
            mergeSort(list2, 0, amount - 1, 1);

            Console.WriteLine(amount);
            for (int t = 0; t < list1.Count; t++)
            {
                string str = list1[t].name;
                while (str.Length < 21)
                    str += ".";
                str += list2[t].name;
                while (str.Length < 42)
                    str += ".";
                Console.WriteLine(str);
            }
        }

        void mergeSort(List<Motor> list, int left, int right, int method)
        {
            if (left >= right)
                return;
            int midPoint = (left + right) / 2;
            mergeSort(list, left, midPoint, method);
            mergeSort(list, midPoint + 1, right, method);
            merge(list, left, midPoint, right, method);
        }

        void merge(List<Motor> list, int left, int midPoint, int right, int method)
        {
            int leftSize = midPoint - left + 1;
            int rightSize = right - midPoint;
            int index = left;

            List<Motor> L = new List<Motor>();
            List<Motor> R = new List<Motor>();
            for (int t = 0; t < leftSize; ++t)
                L.Add(list[left + t]);
            for (int t = 0; t < rightSize; ++t)
                R.Add(list[midPoint + 1 + t]);

            int i = 0, j = 0;
            while (i < leftSize && j < rightSize)
            {
                bool sort;
                if (method == 0)
                {
                    sort = getBool1(L[i], R[j]);
                }
                else
                {
                    sort = getBool2(L[i], R[j]);
                }

                if (sort)
                {
                    list[index]  = L[i];
                    i++;
                }
                else
                {
                    list[index] = R[j];
                    j++;
                }
                index++;
            }

            while (i < leftSize)
            {
                list[index] = L[i];
                i++;
                index++;
            }

            while (j < rightSize)
            {
                list[index] = R[j];
                j++;
                index++;
            }
        }

        public bool getBool1(Motor motor1, Motor motor2)
        {
            if (motor1.price < motor2.price)
            {
                return true;
            }
            else if (motor1.price == motor2.price)
            {
                if (motor1.speed > motor2.speed)
                {
                    return true;
                }
                else if (motor1.speed == motor2.speed)
                {
                    if (motor1.weight < motor2.weight)
                    {
                        return true;
                    }
                    else if (motor1.weight == motor2.weight)
                    {
                        if (string.Compare(motor1.name, motor2.name) <= 0)
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        public bool getBool2(Motor motor1, Motor motor2)
        {
            if (motor1.speed > motor2.speed)
            {
                return true;
            }
            else if (motor1.speed == motor2.speed)
            {
                if (motor1.weight < motor2.weight)
                {
                    return true;
                }
                else if (motor1.weight == motor2.weight)
                {
                    if (motor1.price < motor2.price)
                    {
                        return true;
                    }
                    else if (motor1.price == motor2.price)
                    {
                        if (string.Compare(motor1.name, motor2.name) <= 0)
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }
    }

    public class Motor
    {
        public string name;
        public int price;
        public int speed;
        public int weight;

        public Motor(string _name, int _price, int _speed, int _weight)
        {
            name = _name;
            price = _price;
            speed = _speed;
            weight = _weight;
        }
    }
}
