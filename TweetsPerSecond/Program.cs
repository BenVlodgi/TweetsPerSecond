using System;
using System.Collections.Generic;
using System.Diagnostics;
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
            var testFiles = textFiles.Where(name => name.EndsWith("_input.txt")).ToArray();
            var answerFiles = textFiles.Where(name => name.EndsWith("_output.txt"));

            int choice = -1;
            if (args.Count() > 0)
                int.TryParse(args.First(), out choice);

            while (choice < 0 || choice >= testFiles.Length)
            {
                Console.WriteLine("Choose a test to run");
                Console.WriteLine(string.Join(Environment.NewLine, testFiles.Select((string val, int num) => string.Format("{0}: {1}", num, val.Substring(0, val.LastIndexOf('_'))))));
                Console.Write(">: ");
                int.TryParse(Console.ReadLine(), out choice);
            }

            var contents = File.ReadAllLines(testFiles[choice]);
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

            // Make sure methods are jitted.
            Console.WriteLine(S1.TweetsPerSecond(tps, k).Count());
            Console.WriteLine(S2.TweetsPerSecond(tps, k).Count());
            Console.WriteLine(S3.TweetsPerSecond(tps, k).Count());
            Console.WriteLine(S4.TweetsPerSecond(tps, k).Count());

            int iterations = 100;

            TimeSpan timer1 = TimeSpan.Zero;
            TimeSpan timer2 = TimeSpan.Zero;
            TimeSpan timer3 = TimeSpan.Zero;
            TimeSpan timer4 = TimeSpan.Zero;

            for (int i = 0; i < iterations; i++)
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();

                var stopwatch = Stopwatch.StartNew();
                File.WriteAllLines("out1.txt",S1.TweetsPerSecond(tps, k));
                stopwatch.Stop();
                timer1 += stopwatch.Elapsed;

                GC.Collect();
                GC.WaitForPendingFinalizers();

                stopwatch.Restart();
                File.WriteAllLines("out2.txt",S2.TweetsPerSecond(tps, k));
                stopwatch.Stop();
                timer2 += stopwatch.Elapsed;

                GC.Collect();
                GC.WaitForPendingFinalizers();

                stopwatch.Restart();
                File.WriteAllLines("out3.txt",S3.TweetsPerSecond(tps, k));
                stopwatch.Stop();
                timer3 += stopwatch.Elapsed;

                GC.Collect();
                GC.WaitForPendingFinalizers();

                stopwatch.Restart();
                File.WriteAllLines("out4.txt",S4.TweetsPerSecond(tps, k));
                stopwatch.Stop();
                timer4 += stopwatch.Elapsed;

                Console.WriteLine(i);
            }

            File.WriteAllText("latest_results.txt", string.Format(
                "Timer1: {0} Miliseconds Average, \tTicks: {1}\nTimer2: {2} Miliseconds Average, \tTicks: {3}\nTimer3: {4} Miliseconds Average, \tTicks: {5}",
                TimeSpan.FromTicks(timer1.Ticks / iterations).TotalMilliseconds, timer1.Ticks,
                TimeSpan.FromTicks(timer2.Ticks / iterations).TotalMilliseconds, timer2.Ticks,
                TimeSpan.FromTicks(timer3.Ticks / iterations).TotalMilliseconds, timer3.Ticks));

            Console.WriteLine("Timer1: {0} Miliseconds Average, \tTicks: {1}", TimeSpan.FromTicks(timer1.Ticks / iterations).TotalMilliseconds, timer1.Ticks);
            Console.WriteLine("Timer2: {0} Miliseconds Average, \tTicks: {1}", TimeSpan.FromTicks(timer2.Ticks / iterations).TotalMilliseconds, timer2.Ticks);
            Console.WriteLine("Timer3: {0} Miliseconds Average, \tTicks: {1}", TimeSpan.FromTicks(timer3.Ticks / iterations).TotalMilliseconds, timer3.Ticks);
            Console.WriteLine("Timer4: {0} Miliseconds Average, \tTicks: {1}", TimeSpan.FromTicks(timer4.Ticks / iterations).TotalMilliseconds, timer4.Ticks);
            Console.Write("\nPress Any Key To Finish...");
            Console.ReadKey(true);
        }
    }
}
