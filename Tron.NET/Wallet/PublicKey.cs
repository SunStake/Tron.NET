using System;
using Org.BouncyCastle.Asn1.X9;
using Org.BouncyCastle.Crypto.EC;
using Tron.Utilities;

namespace Tron.Wallet
{
    public readonly struct PublicKey
    {
        private readonly byte[] compressed;
        private readonly byte[] uncompressed;

        private const int UNCOMPRESSED_LENGTH = 65;
        private const int COMPRESSED_LENGTH = 33;
        private const int COOR_LENGTH = 32;

        private const byte EVEN_PREFIX = 2;
        private const byte ODD_PREFIX = 3;
        private const byte UNCOMPRESSED_PREFIX = 4;

        private const string CURVE_NAME = "secp256k1";

        private static X9ECParameters CurvePars = CustomNamedCurves.GetByName(CURVE_NAME);

        public PublicKey(ReadOnlySpan<byte> data)
        {
            if (data.Length == UNCOMPRESSED_LENGTH)
            {
                uncompressed = data.ToArray();
                compressed = Compress(uncompressed);
            }
            else if (data.Length == COMPRESSED_LENGTH)
            {
                compressed = data.ToArray();
                uncompressed = Decompress(compressed);
            }
            else
            {
                throw new ArgumentException($"Invalid public key length: {data.Length}", nameof(data));
            }
        }

        private static byte[] Compress(byte[] data)
        {
            if (data.Length != UNCOMPRESSED_LENGTH)
                throw new ArgumentException($"Uncompressed public key must be {UNCOMPRESSED_LENGTH} bytes", nameof(data));
            if (data[0] != UNCOMPRESSED_PREFIX)
                throw new ArgumentException("Uncompressed prefix missing", nameof(data));

            byte[] compressed = new byte[COMPRESSED_LENGTH];
            compressed[0] = data[data.Length - 1] % 2 == 0 ? EVEN_PREFIX : ODD_PREFIX;
            Array.Copy(data, 1, compressed, 1, COOR_LENGTH);

            return compressed;
        }

        private static byte[] Decompress(byte[] data)
        {
            if (data.Length != COMPRESSED_LENGTH)
                throw new ArgumentException($"Compressed public key must be {COMPRESSED_LENGTH} bytes", nameof(data));
            if (data[0] != ODD_PREFIX && data[0] != EVEN_PREFIX)
                throw new ArgumentException("Compressed prefix missing", nameof(data));

            var point = CurvePars.Curve.DecodePoint(data).Normalize();
            byte[] decompressed = new byte[UNCOMPRESSED_LENGTH];
            decompressed[0] = UNCOMPRESSED_PREFIX;
            Array.Copy(ByteArrayUtilities.PadLeft(point.XCoord.ToBigInteger().ToByteArrayUnsigned(), 32), 0, decompressed, 1, COOR_LENGTH);
            Array.Copy(ByteArrayUtilities.PadLeft(point.YCoord.ToBigInteger().ToByteArrayUnsigned(), 32), 0, decompressed, 1 + COOR_LENGTH, COOR_LENGTH);

            return decompressed;
        }
    }
}
