using System;
using System.Globalization;
using System.Text;

namespace Tron.Utilities
{
    public static class HexUtilities
    {
        public static byte[] HexToBytes(string hex, bool allowHexPrefix = true)
        {
            bool hasHexPrefix = hex.StartsWith("0x");

            if (!allowHexPrefix && hasHexPrefix)
                throw new FormatException("Input hex string contains a hex prefix that's not allowed");
            if (hex.Length % 2 != 0)
                throw new FormatException("Input hex length must be even");

            int charOffset = hasHexPrefix ? 2 : 0;
            int byteCount = hex.Length / 2 - (hasHexPrefix ? 1 : 0);

            byte[] result = new byte[byteCount];

            for (int ind = 0; ind < byteCount; ind++)
                result[ind] = byte.Parse(hex.Substring(ind * 2 + charOffset, 2), NumberStyles.HexNumber);

            return result;
        }

        public static string BytesToHex(byte[] bytes, bool hexPrefix = true)
        {
            var hexBuilder = new StringBuilder();

            if (hexPrefix)
                hexBuilder.Append("0x");

            for (int ind = 0; ind < bytes.Length; ind++)
                hexBuilder.Append(bytes[ind].ToString("x2"));

            return hexBuilder.ToString();
        }
    }
}
