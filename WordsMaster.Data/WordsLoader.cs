using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WordsMaster.Domain.Contracts.Repositories;
using WordsMaster.Domain.Entities;

namespace WordsMaster.Data
{
    public class WordsLoader : IWordsLoader
    {
        private static string WORDS_FOLDER = @"F:\Dev\Private\WordsMaster\WordsMaster\AllWords";
        private ConcurrentDictionary<string, List<string>> _allWords;
        private int _wordCount;

        public WordsLoader()
        {
            _wordCount = 0;
            _allWords = new ConcurrentDictionary<string, List<string>>();
        }


        public int WordCount { get { return _wordCount; } }


        public bool Contains(string strippedWord)
        {
            if (string.IsNullOrEmpty(strippedWord))
                return false;

            var key = strippedWord.Substring(0, 1).ToUpper();
            if (_allWords.ContainsKey(key))
                return _allWords[key].Contains(strippedWord);


            return false;
        }


        public List<string> GetAllWordsContaining(string subword)
        {
            var matches = new ConcurrentBag<string>();

            Parallel.ForEach(_allWords.Keys, key => {
                var list = _allWords[key].Where(word => word.Contains(subword));
                foreach (var item in list)
                    matches.Add(item);
            });
            return matches.ToList();
        }


        public Result LoadAllWords()
        {
            var stopwatch = Stopwatch.StartNew();
            var result = new Result { Passed = true };
            var directory = new DirectoryInfo(WORDS_FOLDER);
            var files = directory.GetFiles("? Words.csv");

            LoadWordsFromFilesInParallel(files);

            Console.WriteLine($"Loaded {_wordCount} words in {stopwatch.ElapsedMilliseconds}ms.");
            return result;
        }


        private void LoadWordsFromFilesInParallel(FileInfo[] files)
        {
            Parallel.ForEach(files, file =>
            {
                var key = file.Name.Substring(0, 1).ToUpper();
                using (var stream = new FileStream(file.FullName, FileMode.Open, FileAccess.Read))
                {
                    using (var textReader = new StreamReader(stream))
                    {
                        var allLines = textReader.ReadToEnd().Split('\n');
                        _allWords[key] = allLines.ToList();

                        lock (this)
                        {
                            _wordCount += _allWords[key].Count();
                        }
                    }
                }
            });
        }
    }
}
