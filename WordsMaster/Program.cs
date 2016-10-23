using StructureMap;
using System.Reflection;
using WordsMaster.Domain.Contracts.Managers;
using System;

namespace WordsMaster
{
    class Program
    {
        static void Main(string[] args)
        {
            DisplayCopyRight();

            var ioc = new Container(new RuntimeRegistry());

            ioc.GetInstance<IWordsManager>().Process(args);

            ioc.GetInstance<IProgramFinisher>().Finish();
        }

        private static void DisplayCopyRight()
        {
            System.Console.WriteLine("\nWORDSMASTER v" + Assembly.GetExecutingAssembly().GetName().Version.ToString() + "\n2016 - digitaldias\n\n");        
        }
    }
}
