using WordsMaster.Domain.Contracts.Verifiers;
using WordsMaster.Domain.Entities;

namespace WordsMaster.Business.Verifier
{
    public class ArgumentVerifier : IArgumentVerifier
    {
        public VerificationResult Verify(string[] args)
        {
            var result = new VerificationResult { Passed = true };

            if (args.Length == 0)
                return new VerificationResult { Passed = false, Message = "No arguments given" };

            if (args.Length > 1)
                return new VerificationResult { Passed = false, Message = "This program only supports one argument" };

            var argument = args[0];
            if (argument.Length > 5)
                return new VerificationResult { Passed = false, Message = $"The argument '{argument}' is too long. Maximum 5 characters!" };

            return new VerificationResult {
                Passed = true,
                Message = string.Empty,
                SubWord = argument
            };
        }
    }
}
