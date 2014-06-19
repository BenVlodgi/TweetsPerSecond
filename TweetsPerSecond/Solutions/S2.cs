using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TweetsPerSecond
{
    /// <summary>
    /// My Second attempt
    /// </summary>
    static class S2
    {
        public static IEnumerable<string> TweetsPerSecond(int[] tweetsPerSecond, int windowSize)
        {
            int head = 0; // Front of the window (where values fall out)
            int max = 0;  // Index with max value in the window
            int check;    // Starting index of list of Nodes to be checked against the max

            string[] maxes = new string[tweetsPerSecond.Length]; // Max values to be printed to the console.
            for (int i = 0; i < tweetsPerSecond.Length; i++)
            {
                check = i;

                // If we have filled up the window with values
                if (i - head + 1 > windowSize)
                {
                    // If the maximum value is falling out of the window
                    if (head == max)
                    {
                        // Maximum value is defaultly set to the first value in the window
                        max = head + 1;
                        check = max + 1;
                    }
                    head++;
                }

                // Start our checking with the set CHECK node, which is typically just the most recently added node, unless our max fell out of the window
                for (int next = check; next <= i && next < tweetsPerSecond.Length; next++)
                    // If this node being checked is higher than our highest found value, then set this node to be the highest (MAX), and check the next.
                    if (tweetsPerSecond[next] > tweetsPerSecond[max])
                        max = next;

                // Store our found max node in an array.
                maxes[i] = tweetsPerSecond[max].ToString();
            }
            return maxes;
        }
    }
}
