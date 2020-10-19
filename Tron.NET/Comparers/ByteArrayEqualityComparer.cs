using System.Collections.Generic;

namespace Tron.Comparers
{
    internal class ByteArrayEqualityComparer : IEqualityComparer<byte[]>
    {
        public static ByteArrayEqualityComparer Instance = new ByteArrayEqualityComparer();

        public bool Equals(byte[] bytes1, byte[] bytes2)
        {
            if (ReferenceEquals(bytes1, null) || ReferenceEquals(bytes2, null))
                return false;
            if (ReferenceEquals(bytes1, bytes2))
                return true;
            if (bytes1.Length != bytes2.Length)
                return false;

            for (int ind = 0; ind < bytes1.Length; ind++)
                if (bytes1[ind] != bytes2[ind])
                    return false;

            return true;
        }

        public int GetHashCode(byte[] bytes)
        {
            int byteSum = 0;
            for (int ind = 0; ind < bytes.Length; ind++)
                byteSum += bytes[ind];

            return byteSum;
        }
    }
}
