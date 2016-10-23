using System;
using WordsMaster.Domain.Contracts.Managers;

namespace WordsMaster.Business.Managers
{
    public class ProgramFinisher : IProgramFinisher
    {
        public void Finish()
        {
            Console.WriteLine("Program finished.");
        }
    }
}
