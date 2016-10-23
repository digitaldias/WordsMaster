using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using WordsMaster.Domain.Contracts.Repositories;

namespace WordsMaster.Console
{
    public class ResultsWriter : IResultsWriter
    {
        public void Write(IEnumerable<string> results)
        {
            var stopwatch = Stopwatch.StartNew();
            var columnCount = System.Console.WindowWidth / 25;
            var finalWordCount = results.Count();
            string output = "\nRESULTS";

            output += "\n" + new string('-', System.Console.WindowWidth) + '\n';
            for (int i = 1; i < results.Count(); i++)
            {
                output += string.Format("{0, -25}", results.ElementAt(i - 1));
                if (i % columnCount == 0)
                    output += "\n";
            }

            output += "\n" + new string('-', System.Console.WindowWidth) + '\n';

            WriteOutputInYellow(output);
            System.Console.WriteLine($"Output written in {stopwatch.ElapsedMilliseconds}ms");
        }


        private void WriteOutputInYellow(string output)
        {
            var originalColor = System.Console.ForegroundColor;
            System.Console.ForegroundColor = ConsoleColor.Yellow;
            System.Console.WriteLine(output);
            System.Console.ForegroundColor = originalColor;
        }

    }
}
