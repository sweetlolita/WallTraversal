using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WallTraversal.Framework.BusinessLayer.Cryption
{
    public class Base64Codec
    {
        public static string encodeFromBytes(byte[] source)
        {
            return Convert.ToBase64String(source);
        }

        public static string encodeFromString(string source)
        {
            return encodeFromBytes(System.Text.Encoding.Default.GetBytes(source));
        }

        public static byte[] decodeAsBytes(string source)
        {
            return Convert.FromBase64String(source);
        }

        public static string decodeAsString(string source)
        {
            return System.Text.Encoding.Default.GetString(decodeAsBytes(source));
        }
    }
}
