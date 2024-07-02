using System;

#nullable disable
public class RijndaelSimpleTest
{
    [STAThread]
    private static void Main(string[] args)
    {
        string plainText = "";
        string cipherText = "BnCxGiN4aJDE+qUe2yIm8Q==";
        string passPhrase = "^F79ejk56$£";
        string saltValue = "DHj47&*)$h";
        string hashAlgorithm = "MD5";
        int passwordIterations = 1024;
        string initVector = "&!£$%^&*()CvHgE!";
        int keySize = 256;
        RijndaelSimple.Encrypt(plainText, passPhrase, saltValue, hashAlgorithm, passwordIterations, initVector, keySize);
        string str = RijndaelSimple.Decrypt(cipherText, passPhrase, saltValue, hashAlgorithm, passwordIterations, initVector, keySize);
        while (true)
        {
            Console.WriteLine("Please enter the password: ");
            if (!(Console.ReadLine() == str))
                Console.WriteLine("Bad Luck! Try again!");
            else
                break;
        }
        Console.WriteLine("Well Done! You cracked it!");
        Console.ReadLine();
    }
}