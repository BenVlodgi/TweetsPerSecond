using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace TweetsPerSecond
{
    /// <summary>
    /// mjolka's implementation
    /// </summary>
    /// http://codereview.stackexchange.com/a/54674/38054
    static class S3
    {
        public static IEnumerable<string> TweetsPerSecond(int[] tweetsPerSecond, int windowSize)
        {
            var size = windowSize + 1;
            var bag = new SortedBag<int>();
            for (var i = 0; i < tweetsPerSecond.Length; i++)
            {
                if (i > windowSize)
                {
                    bag.Remove(tweetsPerSecond[i - size]);
                }

                bag.Add(tweetsPerSecond[i]);
                yield return bag.Max.ToString();
            }
        }

        private class SortedBag<T>
        {
            private readonly Dictionary<T, int> count = new Dictionary<T, int>();
            private readonly SortedSet<T> set = new SortedSet<T>();

            public void Remove(T value)
            {
                count[value] = count[value] - 1;
                if (count[value] == 0)
                {
                    this.set.Remove(value);
                    this.count.Remove(value);
                }
            }

            public void Add(T value)
            {
                int occurrences;
                if (this.count.TryGetValue(value, out occurrences))
                {
                    this.count[value] = occurrences + 1;
                    return;
                }

                this.count[value] = 1;
                this.set.Add(value);
            }

            public T Max
            {
                get { return this.set.Max; }
            }
        }
    }
}
