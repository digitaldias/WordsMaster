using System.Collections.Generic;
using WordsMaster.Domain.Entities;

namespace WordsMaster.Domain.Contracts.Repositories
{
    public interface IWordsLoader
    {
        Result LoadAllWords();

        List<string> GetAllWordsContaining(string subword);
        bool Contains(string strippedWord);
    }
}