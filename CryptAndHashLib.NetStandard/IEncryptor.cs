namespace CryptAndHashLib.NetStandard
{
    public interface IEncryptor
    {
        string GenerateKey();
        string GenerateIV();
        string Encrypt(string plainText);
        string Encrypt(string plainText, byte[] iv);
        string Encrypt(string plainText, string iv);
        string Decrypt(string encryptedString);
    }
}