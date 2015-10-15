namespace BLL.Interfaces
{
    public interface IEncryptorService
    {
        string Encrypt(string stringToEncrypt);
        string Decrypt(string encryptedString);
    }
}