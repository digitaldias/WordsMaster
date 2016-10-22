using StructureMap;
using WordsMaster.Domain.Contracts.Managers;

namespace WordsMaster
{
    class Program
    {
        static void Main(string[] args)
        {
            var ioc = new Container(new RuntimeRegistry());

            ioc.GetInstance<IWordsManager>().Process(args);

            ioc.GetInstance<IProgramFinisher>().Finish();
        }
    }
}
