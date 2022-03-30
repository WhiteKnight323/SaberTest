using System;
using System.Collections.Generic;
using System.IO;

namespace SaberTest
{
    class ListRandom
    {
        public ListNode Head;
        public ListNode Tail;
        public int Count;

        public void Serialize(Stream s)
        {
            List<ListNode> listNodes = new List<ListNode>();
            listNodes.Add(Head);

            for (int i = 0; i < this.Count - 1; i++)
            {
                listNodes.Add(listNodes[i].Next);
            }

            //For serialization, first the index of the Random element in the list is written, and on the next line the data from the Data field

            using (StreamWriter sw = new StreamWriter(s))
            {
                foreach (var node in listNodes)
                {
                    sw.WriteLine(listNodes.IndexOf(node.Random).ToString());
                    sw.WriteLine(node.Data);
                }
            }
        }

        public void Deserialize(Stream s)
        {
            List<ListNode> listNodes = new List<ListNode>();
            ListNode temp = new ListNode();
            Count = 0;

            List<string> randomList = new List<string>(); //This list is needed to store information about the Random field of each list item.
            string nodeString;

            //First, create a doubly linked list with the required number of elements

            using (StreamReader sr = new StreamReader(s))
            {
                while ((nodeString = sr.ReadLine()) != null)
                {
                    Count++;
                    randomList.Add(nodeString);
                    nodeString = sr.ReadLine();
                    temp.Data = nodeString;
                    ListNode next = new ListNode();
                    temp.Next = next;
                    listNodes.Add(temp);
                    next.Previous = temp;
                    temp = next;
                }
                Head = listNodes[0];
                Tail = listNodes[listNodes.Count-1];
                Tail.Next = null;
            }

            //Then, add information about the Random field of each element

            for (int i = 0; i < Count; i++)
            {
                try
                {
                    listNodes[i].Random = listNodes[Int32.Parse(randomList[i])];
                }
                catch (Exception e)
                {
                    Console.WriteLine($"An error occurred on the element {i+1} while assigning a reference to Random: {e.Message}");
                }
            }
        }
    }
}
