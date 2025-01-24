using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _9313
{
    internal class Program
    {
        //student code start






        //student code end
        static void Main(string[] args)
        {
            // Test 1: General case
            Queue<int> q1 = Arr2Q<int>(new int[] { 1, 2, 3, 6, 10 });
            RunTest("Test 1.1 - NextPerfect(q1, 0)", NextPerfect(q1, 0), 3);
            RunTest("Test 1.2 - NextPerfect(q1, 3)", NextPerfect(q1, 3), 6);
            RunTest("Test 1.3 - NextPerfect(q1, 6)", NextPerfect(q1, 6), -1);
            RunQueueTest("Test 1.4 - PerfectQ(q1)", PerfectQ(q1), new int[] { 3, 6 });

            // Test 2: No perfect numbers
            Queue<int> q2 = Arr2Q<int>(new int[] { 1, 2, 4, 8 });
            RunTest("Test 2.1 - NextPerfect(q2, 0)", NextPerfect(q2, 0), -1);
            RunQueueTest("Test 2.2 - PerfectQ(q2)", PerfectQ(q2), new int[] { });

            // Test 3: Single perfect number
            Queue<int> q3 = Arr2Q<int>(new int[] { 1, 2, 3 });
            RunTest("Test 3.1 - NextPerfect(q3, 0)", NextPerfect(q3, 0), 3);
            RunQueueTest("Test 3.2 - PerfectQ(q3)", PerfectQ(q3), new int[] { 3 });

            // Test 4: Empty queue
            Queue<int> q4 = Arr2Q<int>(new int[] { });
            //RunTest("Test 4.1 - NextPerfect(q4, 0)", NextPerfect(q4, 0), -1);
            //RunQueueTest("Test 4.2 - PerfectQ(q4)", PerfectQ(q4), new int[] { });

            // Test 5: Queue with multiple identical perfect numbers
            Queue<int> q5 = Arr2Q<int>(new int[] { 1, 2, 3, 3, 6, 10 });
            RunTest("Test 5.1 - NextPerfect(q5, 0)", NextPerfect(q5, 0), 3);
            RunTest("Test 5.2 - NextPerfect(q5, 3)", NextPerfect(q5, 3), 6);
            RunQueueTest("Test 5.3 - PerfectQ(q5)", PerfectQ(q5), new int[] { 3, 6 });
        }

        // Helper method to convert an array to a queue
        static Queue<T> Arr2Q<T>(T[] arr)
        {
            Queue<T> q = new Queue<T>();
            foreach (T value in arr)
                q.Insert(value);
            return q;
        }

        // Helper method to test single integer return values
        static void RunTest(string testName, int actual, int expected)
        {
            if (actual == expected)
            {
                Console.WriteLine($"{testName}: PASSED");
            }
            else
            {
                Console.WriteLine($"{testName}: FAILED (Expected: {expected}, Actual: {actual})");
            }
        }

        // Helper method to test queue outputs
        static void RunQueueTest(string testName, Queue<int> actual, int[] expectedArr)
        {
            Queue<int> expected = Arr2Q<int>(expectedArr);
            if (AreQueuesEqual(actual, expected))
            {
                Console.WriteLine($"{testName}: PASSED");
            }
            else
            {
                Console.WriteLine($"{testName}: FAILED (Expected: {QueueToString(expected)}, Actual: {QueueToString(actual)})");
            }
        }

        // Helper method to compare two queues
        static bool AreQueuesEqual(Queue<int> q1, Queue<int> q2)
        {
            while (!q1.IsEmpty() && !q2.IsEmpty())
            {
                if (q1.Remove() != q2.Remove())
                    return false;
            }
            return q1.IsEmpty() && q2.IsEmpty();
        }

        // Helper method to convert a queue to a string for comparison
        static string QueueToString(Queue<int> q)
        {
            Queue<int> temp = new Queue<int>();
            string result = "QueueHead[";
            while (!q.IsEmpty())
            {
                int current = q.Remove();
                temp.Insert(current);
                result += current + ",";
            }
            while (!temp.IsEmpty())
                q.Insert(temp.Remove());
            return result.TrimEnd(',') + "]";
        }

        static int NextPerfectGideon(Queue<int> q, int lastPerfect)
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

        static Queue<int> PerfectQGideon(Queue<int> q)
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






    }
}
