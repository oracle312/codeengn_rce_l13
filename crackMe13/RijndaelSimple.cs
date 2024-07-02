using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

#nullable disable
public class RijndaelSimple
{
    public static string Encrypt(
      string plainText,
      string passPhrase,
      string saltValue,
      string hashAlgorithm,
      int passwordIterations,
      string initVector,
      int keySize)
    {
        byte[] bytes1 = Encoding.ASCII.GetBytes(initVector);
        byte[] bytes2 = Encoding.ASCII.GetBytes(saltValue);
        byte[] bytes3 = Encoding.UTF8.GetBytes(plainText);
        byte[] bytes4 = new PasswordDeriveBytes(passPhrase, bytes2, hashAlgorithm, passwordIterations).GetBytes(keySize / 8);
        RijndaelManaged rijndaelManaged = new RijndaelManaged();
        rijndaelManaged.Mode = CipherMode.CBC;
        ICryptoTransform encryptor = rijndaelManaged.CreateEncryptor(bytes4, bytes1);
        MemoryStream memoryStream = new MemoryStream();
        CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, encryptor, CryptoStreamMode.Write);
        cryptoStream.Write(bytes3, 0, bytes3.Length);
        cryptoStream.FlushFinalBlock();
        byte[] array = memoryStream.ToArray();
        memoryStream.Close();
        cryptoStream.Close();
        return Convert.ToBase64String(array);
    }

    public static string Decrypt(
      string cipherText,
      string passPhrase,
      string saltValue,
      string hashAlgorithm,
      int passwordIterations,
      string initVector,
      int keySize)
    {
        byte[] bytes1 = Encoding.ASCII.GetBytes(initVector);
        byte[] bytes2 = Encoding.ASCII.GetBytes(saltValue);
        byte[] buffer = Convert.FromBase64String(cipherText);
        byte[] bytes3 = new PasswordDeriveBytes(passPhrase, bytes2, hashAlgorithm, passwordIterations).GetBytes(keySize / 8);
        RijndaelManaged rijndaelManaged = new RijndaelManaged();
        rijndaelManaged.Mode = CipherMode.CBC;
        ICryptoTransform decryptor = rijndaelManaged.CreateDecryptor(bytes3, bytes1);
        MemoryStream memoryStream = new MemoryStream(buffer);
        CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, decryptor, CryptoStreamMode.Read);
        byte[] numArray = new byte[buffer.Length];
        int count = cryptoStream.Read(numArray, 0, numArray.Length);
        memoryStream.Close();
        cryptoStream.Close();
        return Encoding.UTF8.GetString(numArray, 0, count);
    }
}