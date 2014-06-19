using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TweetsPerSecond
{
    /// <summary>
    /// My initial submition to CR, except for for-loop implementation at the bottom
    /// Author: Benjamin Blodgett
    /// </summary>
    /// http://codereview.stackexchange.com/questions/54662/tweets-per-second-using-linked-list
    static class S1
    {
        class Node
        {
            public Node Next;
            public int Value;
            public override string ToString()
            {
                return string.Format("Value = {0}, Next.IsNull? = {1}", Value, Next == null);
            }
        }

        public static IEnumerable<string> TweetsPerSecond(int[] tweetsPerSecond, int windowSize)
        {
            Node head = null; // Front of the window (where values fall out)
            Node max = null;  // Node with max value in the window
            Node check = null;// Starting node of list of Nodes to be checked against the max
            Node back = null; // End of the window (where new values are put in)

            int count = 0; // Count of numbers in the window
            string[] maxes = new string[tweetsPerSecond.Length]; // Max values to be printed to the console.
            for (int i = 0; i < tweetsPerSecond.Length; i++)
            {
                // If this is our very first number being added, create new head node
                // Else append a new node to the back of the list, and set it to be the next checked node against the max
                if (head == null) max = back = head = new Node() { Value = tweetsPerSecond[i] };
                else check = back = back.Next = new Node() { Value = tweetsPerSecond[i] };

                // If we have filled up the window with values
                if (count + 1 > windowSize)
                {
                    // If the maximum value is falling out of the window
                    if (head == max)
                    {
                        // Maximum value is defaultly set to the first value in the window
                        max = head.Next;
                        // And we set the next nodes to be tested against this new max to be all nodes in the window after the max
                        check = max.Next;
                    }
                    head = head.Next;
                }
                else count++;

                // Start our checking with the set CHECK node, which is typically just the most recently added node, unless our max fell out of the window
                for (Node next = check; next != null; next = next.Next)
                    // If this node being checked is higher than our highest found value, then set this node to be the highest (MAX), and check the next.
                    if (next.Value > max.Value)
                        max = next;

                // Store our found max node in an array.
                maxes[i] = max.Value.ToString();
            }
            return maxes;
        }
    }
}
