using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _9313
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Queue<int> q = Arr2Q<int>(new int[] { 1, 2, 3, 6, 10 });
            int next = NextPerfect(q,0);
            Console.WriteLine(next);
            next = NextPerfect(q, next);
            Console.WriteLine(next);
            next = NextPerfect(q, next);
            Console.WriteLine(next);
            Console.WriteLine(PerfectQ(q));

            //Console.WriteLine(SumPerfect(q));

            /*
            Queue<int> q = Arr2Q<int>(new int[] { 1,2,3,6,10});
            NextPerfectVoid(q);
            Console.WriteLine(q);


            q = Arr2Q<int>(new int[] { 1, 2, 7, 6, 10 });
            NextPerfectVoid(q);
            Console.WriteLine(q);
            */
        }

        static Queue<T> Arr2Q<T>(T[] arr)
        {
            Queue<T> q = new Queue<T>();
            foreach (T value in arr)
                q.Insert(value);
            return q;
        }

        static int NextPerfect(Queue<int> q, int lastPerfect)
        {
            int sum = 0; // sum of numbers till here
            int nextPerfect = -1; // default value. will change if we find a next perfect
            Queue<int> temp = new Queue<int>(); 

            while (!q.IsEmpty())
            {
                int current = q.Remove(); 
                temp.Insert(current); // add head to tmp

                // Only update nextPerfect if it hasn't been set yet
                if (nextPerfect == -1 && current == sum && current > lastPerfect)
                    nextPerfect = current;

                sum += current; // Update the running sum
            }

            // Restore Q
            while (!temp.IsEmpty())
                q.Insert(temp.Remove());

            return nextPerfect; // Return the next perfect number found, or -1 if none
        }

        static Queue<int> PerfectQ(Queue<int> q)
        {
            Queue<int> ret = new Queue<int>();
            int lastPerfect = 0; //start with 0
            int nextPerfect = NextPerfect(q, lastPerfect); //get first Perfect

            while (nextPerfect!=-1) //if a Perfect exists, then:
            {
                ret.Insert(nextPerfect); //add it to ret
                lastPerfect = nextPerfect; //update lastPerfect
                nextPerfect = NextPerfect(q, lastPerfect); //find the next Perfect
            }
            return ret;
        }

        static int SumPerfect(Queue<int> q)
        {
            int sum = 0; // To accumulate the sum of perfect numbers
            int lastPerfect = -1; // Start with an invalid perfect number
            int nextPerfect = NextPerfect(q, lastPerfect); // Find the first perfect number

            while (nextPerfect != -1) // Continue as long as a perfect number is found
            {
                sum += nextPerfect; // Add the perfect number to the sum
                lastPerfect = nextPerfect; // Update the last perfect number
                nextPerfect = NextPerfect(q, lastPerfect); // Find the next perfect number
            }

            return sum; // Return the total sum of perfect numbers
        }



        //remove numbers from q until (not including) the first perfect number. if no perfect number in q, q will be empty after calling
        //example1: before: queuehead->[1,2,3,6,10], after: queuehead->[3,6,10]
        //example2: before: queuehead->[1,2,7,6,10], after: queuehead->[]


        static void NextPerfectVoid(Queue<int> q)
        {
            int sum = 0;
            while (!q.IsEmpty() && sum != q.Head())
                sum += q.Remove();
        }





        public static void NextPerfectOran(Queue<int> q)
        {
            int sum = 0;
            if (!q.IsEmpty())
                sum = (q.Remove() * 2);
            while (!q.IsEmpty() && sum != q.Head())
            {
                sum += q.Remove();
            }
        }


        //return true if a number in index m is the sum of all preceeding numbers in the Queue q
        static bool IsPerfectIndex(Queue<int> q, int m) 
        {
            if (m <= 1) return false; // invalid m

            Queue<int> tmp = new Queue<int>();
            int sumBefore = 0;
            bool isPerfect = false;
            int index = 1; // Track the current position in the queue

            while (!q.IsEmpty())
            {
                int value = q.Remove();

                if (index < m)
                    sumBefore += value; // Add to the sum before the m-th element
                else if (index == m)
                    isPerfect = (value == sumBefore); // Check m-th element

                tmp.Insert(value);
                index++;
            }

            // Restore the original queue
            while (!tmp.IsEmpty())
                q.Insert(tmp.Remove());

            // If m > q size, isPerfect = false
            return isPerfect;
        }




    }
}
