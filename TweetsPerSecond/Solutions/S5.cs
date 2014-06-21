using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TweetsPerSecond
{
    class S5
    {
        public static IEnumerable<string> TweetsPerSecond(int[] tweetsPerSecond, int windowSize)
        {
            var queue = new LinkedList<int>();
            for (var i = 0; i < tweetsPerSecond.Length; i++)
            {
                var tweets = tweetsPerSecond[i];
                while (queue.Last != null && tweets >= tweetsPerSecond[queue.Last.Value])
                {
                    queue.RemoveLast();
                }

                if (i >= windowSize)
                {
                    while (queue.First != null && queue.First.Value <= i - windowSize)
                    {
                        queue.RemoveFirst();
                    }
                }

                queue.AddLast(i);
                yield return tweetsPerSecond[queue.First.Value].ToString();
            }
        }
    }
}
