using Microsoft.AspNetCore.Cryptography.KeyDerivation;
namespace CryptAndHashLib.NetStandard
{
    public interface IHasher
    {
        string Hash(string plainText);
        string Hash(string plainText, int numberOfIterations, KeyDerivationPrf pseudoRandomFunction);
        bool Verify(string plainText, string hash);
        bool Verify(string plainText, string hash, int numberOfIterations, KeyDerivationPrf pseudRandomFunction);
    }
}