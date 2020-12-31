using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

public class Hash
{
    public static string Md5(string str)
    {
        var bytes = Encoding.UTF8.GetBytes(str);
        return Encoding.UTF8.GetString(MD5.Create().ComputeHash(bytes));
    }

    public static string Sha256(string str)
    {
        var bytes = Encoding.UTF8.GetBytes(str);
        return Encoding.UTF8.GetString(SHA256.Create().ComputeHash(bytes));
    }

    public static string Md5File(string filename)
    {
        using (var md5 = MD5.Create())
        {
            using (var stream = File.OpenRead(filename))
            {
                var hash = md5.ComputeHash(stream);
                return BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
            }
        }
    }
}