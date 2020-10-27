using System;
using System.Collections.Generic;
using System.Web;
using System.Security.Cryptography;

/// <summary>
/// Summary description for Bill_Sys_Utility
/// </summary>
public static class Bill_Sys_Utility
{
    public static string GenerateOtp(int length)
	{
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        System.Threading.Thread.Sleep(20);
        Random rnd = new Random();
        char ch;
        int i = 0;
        for (i = 1; i <= length; i++)
        {
            ch = Convert.ToChar(Convert.ToInt32(25 * rnd.NextDouble() + 65));
            sb.Append(ch);
        }
            return sb.ToString();
        }
    public static string ComputeHMACSHA1(string message, string key)
    {
        System.Text.ASCIIEncoding encoding = new System.Text.ASCIIEncoding();
        byte[] keyByte = encoding.GetBytes(key);
        HMACSHA1 hmacsha1 = new HMACSHA1(keyByte);
        byte[] messageBytes = encoding.GetBytes(message);
        byte[] hashmessage = hmacsha1.ComputeHash(messageBytes);
        return ByteToString(hashmessage);
    }

    private static string ByteToString(byte[] buff)
    {
       
        string sbinary = "";
        for (int i = 0; i < buff.Length; i++)
            sbinary += buff[i].ToString("X2");
          return (sbinary);
      
    }
	}
