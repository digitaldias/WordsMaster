using System.Collections.Generic;

namespace WordsMaster.Domain.Contracts.Repositories
{
    public interface IResultsWriter
    {
        void Write(IEnumerable<string> results);
    }
}
