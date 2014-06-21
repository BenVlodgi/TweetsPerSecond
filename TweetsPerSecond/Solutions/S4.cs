using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TweetsPerSecond
{
    class S4
    {
        public static IEnumerable<string> TweetsPerSecond(int[] tweetsPerSecond, int windowSize)
        {
            var skipList = new IntSkipList();
            for (var i = 0; i < tweetsPerSecond.Length; i++)
            {
                if (i >= windowSize)
                {
                    skipList.Remove(tweetsPerSecond[i - windowSize]);
                }

                skipList.Insert(tweetsPerSecond[i]);
                yield return skipList.Max.ToString();
            }
        }
    }
}
