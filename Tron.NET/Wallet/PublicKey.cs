using System;
using Org.BouncyCastle.Asn1.X9;
using Org.BouncyCastle.Crypto.EC;
using Tron.Comparers;
using Tron.Helpers;
using Tron.Utilities;

namespace Tron.Wallet
{
    public readonly struct PublicKey : IEquatable<PublicKey>
    {
        private readonly byte[] compressed;
        private readonly byte[] uncompressed;
        private readonly int hashCode;

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

            hashCode = ByteArrayEqualityComparer.Instance.GetHashCode(compressed);
        }

        public Address ToAddress()
        {
            byte[] addressBytes = new byte[20];
            byte[] keyHash = CryptoHelper.Keccak256(uncompressed, 1, 64);
            Array.Copy(keyHash, 12, addressBytes, 0, 20);

            return new Address(addressBytes);
        }

        public bool Equals(PublicKey other)
        {
            if (ReferenceEquals(other, null))
                return false;
            return ByteArrayEqualityComparer.Instance.Equals(compressed, other.compressed);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(obj, null))
                return false;
            return obj is PublicKey other && Equals(other);
        }

        public override int GetHashCode()
        {
            return hashCode;
        }

        public static bool operator ==(PublicKey leftOperand, PublicKey rightOperand)
        {
            return leftOperand.Equals(rightOperand);
        }

        public static bool operator !=(PublicKey leftOperand, PublicKey rightOperand)
        {
            return !leftOperand.Equals(rightOperand);
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
