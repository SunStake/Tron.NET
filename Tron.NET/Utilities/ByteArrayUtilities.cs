using System;

namespace Tron.Utilities
{
    public static class ByteArrayUtilities
    {
        public static byte[] PadLeft(this byte[] bytes, int totalLength)
        {
            if (bytes.Length == totalLength)
                return bytes;

            if (bytes.Length > totalLength)
                throw new ArgumentException("The input array is too long for padding", nameof(bytes));

            byte[] temp = new byte[totalLength];
            Array.Copy(bytes, 0, temp, totalLength - bytes.Length, bytes.Length);
            return temp;
        }
    }
}
