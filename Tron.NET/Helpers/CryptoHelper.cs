using Org.BouncyCastle.Crypto.Digests;

namespace Tron.Helpers
{
    public static class CryptoHelper
    {
        public static byte[] Keccak256(byte[] data)
        {
            return Keccak256(data, 0, data.Length);
        }

        public static byte[] Keccak256(byte[] data, int offset, int length)
        {
            byte[] buffer = new byte[32];

            var keccakDigest = new KeccakDigest(256);
            keccakDigest.BlockUpdate(data, offset, length);
            keccakDigest.DoFinal(buffer, 0);

            return buffer;
        }
    }
}
