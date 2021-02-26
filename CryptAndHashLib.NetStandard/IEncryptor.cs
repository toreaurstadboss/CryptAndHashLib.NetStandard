namespace CryptAndHashLib.NetStandard
{
    public interface IEncryptor
    {
        string GenerateKey();
        string GenerateIV();
        string Encrypt(string plainText);
        string Decrypt(string encryptedString);
    }
}