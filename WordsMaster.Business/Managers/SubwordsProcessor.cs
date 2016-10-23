using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using WordsMaster.Domain.Contracts.Managers;
using WordsMaster.Domain.Contracts.Repositories;
using WordsMaster.Domain.Entities;

namespace WordsMaster.Business.Managers
{
    public class SubwordsProcessor : ISubwordsProcessor
    {
        private readonly IWordsLoader _wordsLoader;
        private readonly IResultsWriter _resultsWriter;

        private ConcurrentBag<string> _finalList;

        public SubwordsProcessor(IWordsLoader wordsLoader, IResultsWriter resultsWriter)
        {
            _wordsLoader = wordsLoader;
            _resultsWriter = resultsWriter;
            _finalList   = new ConcurrentBag<string>();
        }


        public void Process(string subword)
        {
            var loadResult = LoadWordsAndDisplayErrorOnFail();
            if (!loadResult.Passed)
                return;
            
            var candidateWords = CreateCandidateListFor(subword);

            ExtractWordsWithMeaningfulCandidates(candidateWords);

            _resultsWriter.Write(_finalList.ToList().OrderBy(word => word));
        }


        private Result LoadWordsAndDisplayErrorOnFail()
        {
            var result = _wordsLoader.LoadAllWords();

            if (!result.Passed)
                Console.WriteLine(result.Message);

            return result;
        }

        private void ExtractWordsWithMeaningfulCandidates(List<string> candidateWords)
        {
            var stopWatch = Stopwatch.StartNew();
            Parallel.ForEach(candidateWords, candidate =>
            {
                var leftHalf = candidate.Split(':')[0];
                var rightHalf = candidate.Split(':')[1];

                if (_wordsLoader.Contains(rightHalf))
                {
                    if (!leftHalf.StartsWith(rightHalf) && !leftHalf.EndsWith(rightHalf))                        
                        _finalList.Add(candidate);
                }
            });
            Console.WriteLine($"{candidateWords.Count()} candidates resulted in {_finalList.Count()} matches. This took {stopWatch.ElapsedMilliseconds} ms");
        }

        private List<string> CreateCandidateListFor(string subword)
        {
            var wordsContainingSubword = _wordsLoader.GetAllWordsContaining(subword);

            return wordsContainingSubword.Select(word => word + ":" + word.Replace(subword, "")).ToList();
        }
    }
}
