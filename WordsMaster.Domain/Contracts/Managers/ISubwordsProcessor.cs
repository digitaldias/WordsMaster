using WordsMaster.Domain.Contracts.Repositories;

namespace WordsMaster.Domain.Contracts.Managers
{
    public interface ISubwordsProcessor
    {
        void Process(string subword);
    }
}
