using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.IO;

    public class Security
    {
        public static string Encrypt(string pwd, string text){

            byte[] pwdByte = Encoding.UTF8.GetBytes(pwd);
            byte[] iv = {0x05, 0x56, 0x78, 0x55, 0x86, 0x79, 0x77, 0x99};
            byte[] textByte = Encoding.UTF8.GetBytes(text);

            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            MemoryStream memoryStream = new MemoryStream();
            CryptoStream crypto = new CryptoStream(memoryStream, des.CreateEncryptor(pwdByte, iv), CryptoStreamMode.Write);

            crypto.Write(textByte, 0 , textByte.Length);
            crypto.FlushFinalBlock();
            crypto.Dispose();
            crypto.Close();

            return Convert.ToBase64String(memoryStream.ToArray());

        }

        public static string Decrypt(string pwd, string text){

            byte[] pwdByte = Encoding.UTF8.GetBytes(pwd);
            byte[] iv = {0x05, 0x56, 0x78, 0x55, 0x86, 0x79, 0x77, 0x99};
            byte[] textByte = Convert.FromBase64String(text);
  
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            MemoryStream memoryStream = new MemoryStream();
            CryptoStream crypto = new CryptoStream(memoryStream, des.CreateDecryptor(pwdByte, iv), CryptoStreamMode.Write);

            crypto.Write(textByte, 0 , textByte.Length);
            crypto.FlushFinalBlock();
            crypto.Dispose();
            crypto.Close();

            return Encoding.UTF8.GetString(memoryStream.ToArray());

        }
    }
