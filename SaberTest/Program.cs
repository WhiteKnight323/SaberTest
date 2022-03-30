using System;
using System.Collections.Generic;
using System.IO;

namespace SaberTest
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string path = "Data.txt"; //Path, where the file will be saved
            ListRandom serializeList = CreateListRandom(10);

            FileStream stream = new FileStream(path, FileMode.OpenOrCreate);
            serializeList.Serialize(stream);
            stream.Close();


            ListRandom deserializeList = new ListRandom();

            FileStream stream2 = new FileStream(path, FileMode.Open);
            deserializeList.Deserialize(stream2);
            stream2.Close();

            //List Comparing

            ListCompare(serializeList, deserializeList);

            Console.ReadLine();
        }
        public static ListRandom CreateListRandom(int numbersOfListNode) //This is the method to create a ListRandom with a given number of items in the list
        {
            ListRandom listRandom = new ListRandom();
            listRandom.Count = numbersOfListNode;
            List<ListNode> listNodes = new List<ListNode>(); //This collection is needed to record all the elements of the list, so that later you can assign a Random to each element of the list
            ListNode temp = new ListNode();
            var rand = new Random();

            for (int i = 0; i < numbersOfListNode; i++)
            {
                temp.Data = DateTime.Now.ToString() + i; //The random data format I took time, but since the list is generated very quickly, I also added an i variable
                ListNode next = new ListNode();
                next.Previous = temp;
                temp.Next = next;
                listNodes.Add(temp);
                temp = next;
            }

            foreach (var node in listNodes)
            {
                node.Random = listNodes[rand.Next(listRandom.Count)];//Here, each element of the list is added with a reference to a random element of the list
            }

            listRandom.Head = listNodes[0];
            listRandom.Tail = listNodes[listNodes.Count - 1];
            listRandom.Tail.Next = null;

            return listRandom;
        }
        public static void ListCompare(ListRandom List1, ListRandom List2)
        {
            List<ListNode> ser = new List<ListNode>();
            List<ListNode> deser = new List<ListNode>();

            ser.Add(List1.Head);
            deser.Add(List2.Head);

            //2 cycles, in case the lists have different length

            for (int i = 0; i < List1.Count - 1; i++)
            {
                ser.Add(ser[i].Next);
            }

            for (int i = 0; i < List2.Count - 1; i++)
            {
                deser.Add(deser[i].Next);
            }

            if (ser.Count != deser.Count)
            {
                Console.WriteLine("Lists are different");
                Console.WriteLine($"Nuber of serializeList elements: {ser.Count}");
                Console.WriteLine($"Nuber of deserializeList elements: {deser.Count}");
            }
            else
            {
                int numOfSameElements = default;
                string s = default;
                for (int i = 0; i < ser.Count; i++)
                {
                    if (ser.IndexOf(ser[i].Random) != deser.IndexOf(deser[i].Random))
                    {
                        s += $"In {i + 1} element different Random value\n";
                    }
                    else if(ser[i].Data != deser[i].Data)
                    {
                        s += $"In {i + 1} element different Data value\n";
                    }
                    else
                    {
                        numOfSameElements++;
                    }
                }
                if (numOfSameElements == ser.Count)
                {
                    Console.WriteLine("Lists are same");
                }
                else
                {
                    Console.WriteLine($"Lists are different\n{s}");
                }
            }
        }
    }
}
