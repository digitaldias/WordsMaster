using System;
using WordsMaster.Domain.Contracts.Managers;
using WordsMaster.Domain.Contracts.Repositories;
using WordsMaster.Domain.Contracts.Verifiers;
using WordsMaster.Domain.Entities;

namespace WordsMaster.Business.Managers
{
    public class WordsManager : IWordsManager
    {
        private readonly IArgumentVerifier  _argumentsVerifier;
        private readonly ISubwordsProcessor _subwordsProcessor;        

        public WordsManager(IArgumentVerifier argumentsVerifier, ISubwordsProcessor subwordsProcessor)
        {
            _argumentsVerifier  = argumentsVerifier;            
            _subwordsProcessor  = subwordsProcessor;
        }

        
        public void Process(string[] arguments)
        {
            var argumentsResult = ValidateArguments(arguments);
            if (!argumentsResult.Passed)
                return;

            _subwordsProcessor.Process(argumentsResult.SubWord);
        }


        private VerificationResult ValidateArguments(string[] arguments)
        {
            var verificationResult = _argumentsVerifier.Verify(arguments);

            if (!verificationResult.Passed)            
                Console.WriteLine(verificationResult.Message);            

            return verificationResult;
        }
    }
}
