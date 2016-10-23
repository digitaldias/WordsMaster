using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CreateWordFiles
{
    class Program
    {
        static void Main(string[] args)
        {
            if(args.Length != 2)
            {
                DisplaySyntax();
                return;
            }

            var inputFile = new FileInfo(args[0]);
            var outputFolder = new DirectoryInfo(args[1]);

            if(!inputFile.Exists || !outputFolder.Exists)
            {
                Console.WriteLine("Something is wrong in the arguments");
                DisplaySyntax();
                return;
            }

            var allLines = LoadFileContents(inputFile);
            GenerateMultipleFiles(allLines, outputFolder);

            Console.WriteLine("Done");
        }


        private static List<string> LoadFileContents(FileInfo inputFile)
        {
            var stopwatch = Stopwatch.StartNew();
            var finalList = new List<string>();
            Console.WriteLine("Loading text from " + inputFile.FullName);
            using (var stream = new FileStream(inputFile.FullName, FileMode.Open, FileAccess.Read))
            {
                using (var streamReader = new StreamReader(stream))
                {
                    var allText = streamReader.ReadToEnd();
                    finalList = new List<string>(allText.Split('\n').OrderBy(word => word).ToList());                    
                }
            }
            Console.WriteLine($"{finalList.Count()} words loaded in {stopwatch.ElapsedMilliseconds}ms");
            return finalList;
        }

        private static void GenerateMultipleFiles(IEnumerable<string> allLines, DirectoryInfo folderInfo)
        {
            var stopwatch = Stopwatch.StartNew();
            Console.WriteLine("Generating output files");
            var dictionary = new ConcurrentDictionary<string, List<string>>();
            var myLock = new object();

            Parallel.ForEach(allLines, line => 
            {
                if (string.IsNullOrEmpty(line))
                    return;

                if (char.IsLetter(line[0]))
                {
                    var firstCharacter = line.Substring(0, 1);
                    var key = firstCharacter.ToUpper();

                    lock (myLock)
                    {
                        if (!dictionary.ContainsKey(key))
                        dictionary[key] = new List<string>();
                    
                        dictionary[key].Add(line);
                    }
                }
            });

            Parallel.ForEach(dictionary.Keys, key => {
                var fileName = Path.Combine(folderInfo.FullName, $"{key} Words.txt");
                var text = string.Join("\n", dictionary[key]);
                File.WriteAllText(fileName, text);
            });
            Console.WriteLine($"{dictionary.Keys.Count()} files saved in {stopwatch.ElapsedMilliseconds}ms");            
        }

        private static void DisplaySyntax()
        {
            Console.WriteLine("CreateWordFiles v" + Assembly.GetExecutingAssembly().GetName().Version + "\n(C)2016 digitaldias\n\n");
            Console.WriteLine("Syntax:\n\nCreateWordFiles <SourceFile> <DestiantionFolder>\n");
        }
    }
}
