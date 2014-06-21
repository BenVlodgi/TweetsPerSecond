using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace TweetsPerSecond
{
    class Program
    {
        //http://www.talentbuddy.co/challenge/52a9121cc8a6c2dc91481f8d5233cc274af0110af382f40f
        static void Main(string[] args)
        {

            if (!Directory.Exists("Tests"))
            {
                Console.Write("There is no Tests Directory\nPress any key to finish...");
                Console.ReadKey(true);
                return;
            }

            var textFiles = Directory.GetFiles("Tests").Where(name => name.EndsWith(".txt"));
            var testFiles = textFiles.Where(name => name.EndsWith("_input.txt"));
            var answerFiles = textFiles.Where(name => name.EndsWith("_output.txt"));

            int choice = -1;
            if (args.Count() > 0)
                int.TryParse(args.First(), out choice);

            while (choice < 0 || choice > testFiles.Count())
            {
                Console.WriteLine("Choose a test to run");
                Console.WriteLine(string.Join(Environment.NewLine, textFiles.Select((string val, int num) => string.Format("{0}: {1}", num, val.Substring(0, val.LastIndexOf('_'))))));
                Console.Write(">: ");
                int.TryParse(Console.ReadLine(), out choice);
            }

            var contents = File.ReadAllLines(testFiles.ToArray()[choice]);
            int k;
            int[] tps;
            try
            {
                k = int.Parse(contents[0]);
                tps = contents[1].Split(',').Select(str => int.Parse(str)).ToArray();
            }
            catch
            {
                Console.WriteLine("Bad test file.");
                return;
            }

            DateTime b1, b2, b3, s1, s2, s3;
            int iterations = 100;

            TimeSpan timer1 = new TimeSpan(0);
            TimeSpan timer2 = new TimeSpan(0);
            TimeSpan timer3 = new TimeSpan(0);

            for (int i = 0; i < iterations; i++)
            {
                b1 = DateTime.Now;
                File.WriteAllLines("out1.txt",S1.TweetsPerSecond(tps, k));
                s1 = DateTime.Now;
                timer1 += s1 - b1;

                b2 = DateTime.Now;
                File.WriteAllLines("out2.txt",S2.TweetsPerSecond(tps, k));
                s2 = DateTime.Now;
                timer2 += s2 - b2;

                b3 = DateTime.Now;
                File.WriteAllLines("out3.txt",S3.TweetsPerSecond(tps, k));
                s3 = DateTime.Now;
                timer3 += s3 - b3;

                Console.WriteLine(i);
            }

            File.WriteAllText("latest_results.txt", string.Format(
                "Timer1: {0} Miliseconds Average, \tTicks: {1}\nTimer2: {2} Miliseconds Average, \tTicks: {3}\nTimer3: {4} Miliseconds Average, \tTicks: {5}",
                new TimeSpan(timer1.Ticks / iterations).TotalMilliseconds, timer1.Ticks,
                new TimeSpan(timer2.Ticks / iterations).TotalMilliseconds, timer2.Ticks,
                new TimeSpan(timer3.Ticks / iterations).TotalMilliseconds, timer3.Ticks));

            Console.WriteLine("Timer1: {0} Miliseconds Average, \tTicks: {1}", new TimeSpan(timer1.Ticks / iterations).TotalMilliseconds, timer1.Ticks);
            Console.WriteLine("Timer2: {0} Miliseconds Average, \tTicks: {1}", new TimeSpan(timer2.Ticks / iterations).TotalMilliseconds, timer2.Ticks);
            Console.WriteLine("Timer3: {0} Miliseconds Average, \tTicks: {1}", new TimeSpan(timer3.Ticks / iterations).TotalMilliseconds, timer3.Ticks);
            Console.Write("\nPress Any Key To Finish...");
            Console.ReadKey(true);
        }
    }
}
