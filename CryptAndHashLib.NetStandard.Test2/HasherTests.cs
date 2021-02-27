using FluentAssertions;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using NUnit.Framework;

namespace CryptAndHashLib.NetStandard.Test
{

    [TestFixture]
    public class HasherTests
    {

        [Test]
        [TestCase("p@ssW0rd", true)]
        public void HashAndVerifySucceeds(string password, bool expectedOutcome)
        {
            var hasher = new Hasher();
            string hashedPassword = hasher.Hash(password);
            bool isVerified = hasher.Verify(password, hashedPassword);
            isVerified.Should().Be(expectedOutcome);
            hashedPassword = hashedPassword.ToUpper();
            isVerified = hasher.Verify(password, hashedPassword);
            isVerified.Should().Be(false);
        }

        [Test]
        [TestCase("p@ssW0rd", true, 10000, KeyDerivationPrf.HMACSHA512)]
        public void HashAndVerifyWithCustomIterationAndPseudoRandomFunctionSucceeds(string password, bool expectedOutcome,
            int numerOfIterations, KeyDerivationPrf pseudoRandomFunction)
        {
            var hasher = new Hasher();
            string hashedPassword = hasher.Hash(password, numerOfIterations, pseudoRandomFunction);
            bool isVerified = hasher.Verify(password, hashedPassword, numerOfIterations, pseudoRandomFunction);
            isVerified.Should().Be(expectedOutcome);
        }

    }
}
