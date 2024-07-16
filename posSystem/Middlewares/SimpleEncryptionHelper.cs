﻿using System;
using System.Text;

namespace posSystem.Middlewares
{
    public static class SimpleEncryptionHelper
    {
        public static string Encrypt(string plainText)
        {
            byte[] plainBytes = Encoding.UTF8.GetBytes(plainText);
            return Convert.ToBase64String(plainBytes);
        }

        public static string Decrypt(string cipherText)
        {
            byte[] cipherBytes = Convert.FromBase64String(cipherText);
            return Encoding.UTF8.GetString(cipherBytes);
        }
    }
}
