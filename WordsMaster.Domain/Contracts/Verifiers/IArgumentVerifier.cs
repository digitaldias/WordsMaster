using WordsMaster.Domain.Entities;

namespace WordsMaster.Domain.Contracts.Verifiers
{
    public interface IArgumentVerifier
    {
        VerificationResult Verify(string[] args);
    }
}
