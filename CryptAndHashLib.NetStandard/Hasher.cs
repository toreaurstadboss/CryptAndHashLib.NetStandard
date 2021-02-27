using System;
using System.Linq;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace CryptAndHashLib.NetStandard
{
    public class Hasher : IHasher
    {
        public string Hash(string plainText)
        {
            //Default running 10,000 iterations with HMACSHA1 for compability with .NET Framework. 
            return Hash(plainText, 10000, KeyDerivationPrf.HMACSHA1);
        }

        public string Hash(string plainText, int numberOfIterations, KeyDerivationPrf pseudoRandomFunction)
        {
            var saltBytes = Generate128BitSalt();

            //256-bit hash
            var hashBytes = KeyDerivation.Pbkdf2(
                password: plainText,
                salt: saltBytes,
                prf: pseudoRandomFunction,
                iterationCount: numberOfIterations,
                numBytesRequested: 256 / 8);

            // Use space to make it easy, but can just use counting
            return String.Concat(Convert.ToBase64String(saltBytes), " ", Convert.ToBase64String(hashBytes));
        }

        public bool Verify(string plainText, string hash)
        {
            return Verify(plainText, hash, 10000, KeyDerivationPrf.HMACSHA1);
        }

        public bool Verify(string plainText, string hash, int numberOfIterations, KeyDerivationPrf pseudRandomFunction)
        {
            var splitHash = hash.Split(' ');
            var saltBytes = Convert.FromBase64String(splitHash[0]);
            var hashBytes = Convert.FromBase64String(splitHash[1]);

            //256-bit hash
            var newHashBytes = KeyDerivation.Pbkdf2(
                password: plainText,
                salt: saltBytes,
                prf: pseudRandomFunction,
                iterationCount: numberOfIterations,
                numBytesRequested: 256 / 8);

            return hashBytes.SequenceEqual(newHashBytes); //using Linq to compare hash bytes with generated hash bytes to compare for equality and verify the hash
        }

        private byte[] Generate128BitSalt()
        {
            var salt = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }
            return salt;
        }

    }
}