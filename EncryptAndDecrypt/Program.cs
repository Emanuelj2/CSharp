
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;


public class Security
{
    
    static string Encrypt(string plainText, byte[] key, byte[] iv)
    {
        using Aes aes = Aes.Create();
        aes.Key = key;
        aes.IV = iv;

        byte[] plainBytes = Encoding.UTF8.GetBytes(plainText);

        using ICryptoTransform encryptor = aes.CreateEncryptor();

        byte[] encryptedBytes = encryptor.TransformFinalBlock(plainBytes, 0, plainBytes.Length);

        return Convert.ToBase64String(encryptedBytes);
    }


    static string Decrypt(string encryptedText, byte[] key, byte[] iv)
    {
        using Aes aes = Aes.Create();
        aes.Key = key;
        aes.IV = iv;

        byte[] encryptedBytes = Convert.FromBase64String(encryptedText);

        using ICryptoTransform decryptor = aes.CreateDecryptor();

        byte[] decryptedBytes = decryptor.TransformFinalBlock(encryptedBytes, 0, encryptedBytes.Length);

        return Encoding.UTF8.GetString(decryptedBytes);
    }

    static void Main(string[] args)
    {
        using Aes aes = Aes.Create();

        byte[] key = aes.Key;
        byte[] iv = aes.IV;

        string originalText = "Patient information";

        // Encrypt
        string encrypted = Encrypt(originalText, key, iv);

        // Decrypt
        string decrypted = Decrypt(encrypted, key, iv);

        Console.WriteLine("Original:");
        Console.WriteLine(originalText);

        Console.WriteLine("\nEncrypted:");
        Console.WriteLine(encrypted);

        Console.WriteLine("\nDecrypted:");
        Console.WriteLine(decrypted);
    }

}
