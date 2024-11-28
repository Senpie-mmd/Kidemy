namespace Kidemy.Application.Services.Interfaces
{
    public interface IEncryptService
    {
        string DecryptText(string cipherText);
        string EncryptText(string plainText);
    }
}