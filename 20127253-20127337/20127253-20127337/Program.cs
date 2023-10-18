using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

public class AES
{
    public static void FileEncrypt(string inputFile, string outputFile, byte[] key)
    {
        //Set Rijndael symmetric encryption algorithm
        RijndaelManaged AES = new RijndaelManaged();

        AES.KeySize = 256;
        AES.BlockSize = 128;
        AES.Padding = PaddingMode.PKCS7;
        AES.Mode = CipherMode.CFB;

        AES.Key = key;
        AES.IV = new byte[16];

        FileStream fsCrypt = new FileStream(outputFile, FileMode.Create);

        CryptoStream cs = new CryptoStream(fsCrypt, AES.CreateEncryptor(), CryptoStreamMode.Write);

        FileStream fsIn = new FileStream(inputFile, FileMode.Open);

        byte[] buffer = new byte[1048576];
        int read;

        try
        {
            while ((read = fsIn.Read(buffer, 0, buffer.Length)) > 0)
            {
                Application.DoEvents();
                cs.Write(buffer, 0, read);
            }

            fsIn.Close();
            Globals.encSuccess = 1;
        }
        catch (Exception ex)
        {
            MessageBox.Show("Error: " + ex.Message);
            Globals.encSuccess = 0;
        }
        finally
        {
            cs.Close();
            fsCrypt.Close();
        }
    }

    public static void FileDecrypt(string inputFile, string outputFile, byte[] key)
    {
        RijndaelManaged AES = new RijndaelManaged();

        AES.KeySize = 256;
        AES.BlockSize = 128;
        AES.Padding = PaddingMode.PKCS7;
        AES.Mode = CipherMode.CFB;

        AES.Key = key;
        AES.IV = new byte[16];

        FileStream fsCrypt = new FileStream(inputFile, FileMode.Open);

        CryptoStream cs = new CryptoStream(fsCrypt, AES.CreateDecryptor(), CryptoStreamMode.Read);

        FileStream fsOut = new FileStream(outputFile, FileMode.Create);

        int read;
        byte[] buffer = new byte[1048576];

        try
        {
            while ((read = cs.Read(buffer, 0, buffer.Length)) > 0)
            {
                Application.DoEvents();
                fsOut.Write(buffer, 0, read);
            }
            Globals.decSuccess = 1;
        }
        catch (CryptographicException ex_CryptographicException)
        {
            MessageBox.Show("CryptographicException error: " + ex_CryptographicException.Message);
            Globals.decSuccess = 0;
        }
        catch (Exception ex)
        {
            MessageBox.Show("Error: " + ex.Message);
            Globals.decSuccess = 0;
        }

        try
        {
            cs.Close();
        }
        catch (Exception ex)
        {
            MessageBox.Show("Error by closing CryptoStream: " + ex.Message);
            Globals.decSuccess = 0;
        }
        finally
        {
            fsOut.Close();
            fsCrypt.Close();
        }
    }
}

public class HASH
{
    public static byte[] HashSHA256(byte[] data)
    {
        using (HashAlgorithm algorithm = SHA256.Create())
            return algorithm.ComputeHash(data);
    }
}

public class DynamicPassword
{
    public static string passwordProccesing(string password)
    {
        int length = password.Length;
        int first = Math.Abs((int)(password[length - 1] - password[length - 2]));
        int second = Math.Abs((int)(password[first] - password[first + 1]));
        int last = Math.Abs((int)(password[second + 1 + Math.Abs(first - second)] - password[second + 1 + Math.Abs(first - second) + 1]));

        return first.ToString() + second.ToString() + last.ToString();
    }

    public static bool checkDynamicPassword(string password)
    {
        if (password == "357")
            return true;
        else
            return false;
    }
}

static class Globals
{
    public static int correct = 0;
    public static int encSuccess = 1;
    public static int decSuccess = 1;
}

namespace _20127253_20127337
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FormMenu());
        }
    }
}
