using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

public static class Cipher
{
    public const string ADDED_EXTENSION = ".securelocked";

    public static void EncryptFile(string file, string password)
    {
        var bytes = File.ReadAllBytes(file);
        var encrypted = Encrypt(bytes, password);

        File.Delete(file);
        File.WriteAllBytes(file + ADDED_EXTENSION, encrypted);

        bytes = null;
        encrypted = null;
    }

    public static void DecryptFile(string file, string password)
    {
        var bytes = File.ReadAllBytes(file);
        var decrypted = Decrypt(bytes, password);
        var path = file.Remove(file.Length - ADDED_EXTENSION.Length);

        File.Delete(file);
        File.WriteAllBytes(path, decrypted);

        bytes = null;
        decrypted = null;
    }

    /// <summary>
    /// Encrypt a string.
    /// </summary>
    /// <param name="plainText">String to be encrypted</param>
    /// <param name="password">Password</param>
    public static byte[] Encrypt(byte[] bytes, string password)
    {
        if (bytes == null)
        {
            return null;
        }

        if (password == null)
        {
            password = String.Empty;
        }

        // Get the bytes of the string
        var bytesToBeEncrypted = bytes;
        var passwordBytes = Encoding.UTF8.GetBytes(password);

        // Hash the password with SHA256
        passwordBytes = SHA256.Create().ComputeHash(passwordBytes);

        var bytesEncrypted = Cipher.Encrypt(bytesToBeEncrypted, passwordBytes);

        return bytesEncrypted;
    }

    /// <summary>
    /// Decrypt a string.
    /// </summary>
    /// <param name="encryptedText">String to be decrypted</param>
    /// <param name="password">Password used during encryption</param>
    /// <exception cref="FormatException"></exception>
    public static byte[] Decrypt(byte[] bytes, string password)
    {
        if (bytes == null)
        {
            return null;
        }

        if (password == null)
        {
            password = String.Empty;
        }

        // Get the bytes of the string
        var bytesToBeDecrypted = bytes;
        var passwordBytes = Encoding.UTF8.GetBytes(password);

        passwordBytes = SHA256.Create().ComputeHash(passwordBytes);

        var bytesDecrypted = Cipher.Decrypt(bytesToBeDecrypted, passwordBytes);

        return bytesDecrypted;
    }

    private static byte[] Encrypt(byte[] bytesToBeEncrypted, byte[] passwordBytes)
    {
        byte[] encryptedBytes = null;

        // Set your salt here, change it to meet your flavor:
        // The salt bytes must be at least 8 bytes.
        var saltBytes = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 };

        using (MemoryStream ms = new MemoryStream())
        {
            using (RijndaelManaged AES = new RijndaelManaged())
            {
                var key = new Rfc2898DeriveBytes(passwordBytes, saltBytes, 1000);

                AES.KeySize = 256;
                AES.BlockSize = 128;
                AES.Key = key.GetBytes(AES.KeySize / 8);
                AES.IV = key.GetBytes(AES.BlockSize / 8);

                AES.Mode = CipherMode.CBC;

                using (var cs = new CryptoStream(ms, AES.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(bytesToBeEncrypted, 0, bytesToBeEncrypted.Length);
                    cs.Close();
                }

                encryptedBytes = ms.ToArray();
            }
        }

        return encryptedBytes;
    }

    private static byte[] Decrypt(byte[] bytesToBeDecrypted, byte[] passwordBytes)
    {
        byte[] decryptedBytes = null;

        // Set your salt here, change it to meet your flavor:
        // The salt bytes must be at least 8 bytes.
        var saltBytes = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 };

        using (MemoryStream ms = new MemoryStream())
        {
            using (RijndaelManaged AES = new RijndaelManaged())
            {
                var key = new Rfc2898DeriveBytes(passwordBytes, saltBytes, 1000);

                AES.KeySize = 256;
                AES.BlockSize = 128;
                AES.Key = key.GetBytes(AES.KeySize / 8);
                AES.IV = key.GetBytes(AES.BlockSize / 8);
                AES.Mode = CipherMode.CBC;

                using (var cs = new CryptoStream(ms, AES.CreateDecryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(bytesToBeDecrypted, 0, bytesToBeDecrypted.Length);
                    cs.Close();
                }

                decryptedBytes = ms.ToArray();
            }
        }

        return decryptedBytes;
    }
}
