using System;
using FluentAssertions;
using NUnit.Framework;

namespace CryptAndHashLib.NetStandard.Test
{

    [TestFixture]
    public class CryptoTests
    {
        private IEncryptor Encryptor { get; set; }

        [SetUp]
        public void TestInitialize()
        {
            Encryptor = new Encryptor();
        }

        [Test]
        public void GenerateKeyReturnsAKey()
        {
            string generateKey = Encryptor.GenerateKey();
            Console.WriteLine($"Here is the sample generated key for the Aes algorithm: {generateKey}");
            generateKey.Should().NotBeNullOrWhiteSpace("Expected to generate a non empty key");
        }

        [Test]
        public void GenerateIvReturnsAnInitializationVector()
        {
            string iv = Encryptor.GenerateIV();
            Console.WriteLine($"Here is the sample generated initialization vector (iv) for the Aes algorithm: {iv}");
            iv.Should().NotBeNullOrWhiteSpace("Expected to generate a non empty iv");
        }

        [Test]
        [TestCase(@"Mars Perseverance Rover 2020 has landed in Jezero crater!")]
        public void CryptAndDecryptWithGeneratedIvDoesNotFail(string input)
        {
            string encryptedText = Encryptor.Encrypt(input);
            string decryptedText = Encryptor.Decrypt(encryptedText);
            decryptedText.Should().Be(input, "Expected to encrypt and decrypt with generated IV using Aes algorithm as a reversible process and two-way function.");
        }

        [Test]
        [TestCase(@"Mars Perseverance Rover 2020 has landed in Jezero crater!")]
        public void CryptAndDecryptWithSpecifiedIvDoesNotFail(string input)
        {
            string iv = "HwJ5Iq5pdhEmZHOdy70VEA==";
            string encryptedText = Encryptor.Encrypt(input, iv);
            string decryptedText = Encryptor.Decrypt(encryptedText);
            decryptedText.Should().Be(input, "Expected to encrypt and decrypt with generated IV using Aes algorithm as a reversible process and two-way function.");
        }

    }
}
