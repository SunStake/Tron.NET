using System;
using Tron.Comparers;

namespace Tron.Wallet
{
    public readonly struct PrivateKey : IEquatable<PrivateKey>
    {
        private readonly byte[] data;
        private readonly int hashCode;

        private const int KEY_LENGTH_IN_BYTES = 32;

        public ReadOnlySpan<byte> Data => data;

        public PrivateKey(ReadOnlySpan<byte> data)
        {
            if (data.Length != KEY_LENGTH_IN_BYTES)
                throw new ArgumentException($"Length of {nameof(data)} must be {KEY_LENGTH_IN_BYTES} bytes", nameof(data));

            this.data = new byte[KEY_LENGTH_IN_BYTES];
            data.CopyTo(this.data);
            hashCode = ByteArrayEqualityComparer.Instance.GetHashCode(this.data);
        }

        public static PrivateKey FromByteArray(byte[] data, int offset = 0)
        {
            if (data.Length < offset + KEY_LENGTH_IN_BYTES)
                throw new ArgumentException($"Length of {nameof(data)} must be at least {offset + KEY_LENGTH_IN_BYTES} bytes", nameof(data));

            return new PrivateKey((new ReadOnlySpan<byte>(data, offset, KEY_LENGTH_IN_BYTES)));
        }

        public bool Equals(PrivateKey other)
        {
            if (ReferenceEquals(other, null))
                return false;
            return ByteArrayEqualityComparer.Instance.Equals(data, other.data);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(obj, null))
                return false;
            return obj is PrivateKey other && Equals(other);
        }

        public override int GetHashCode()
        {
            return hashCode;
        }

        public static bool operator ==(PrivateKey leftOperand, PrivateKey rightOperand)
        {
            return leftOperand.Equals(rightOperand);
        }

        public static bool operator !=(PrivateKey leftOperand, PrivateKey rightOperand)
        {
            return !leftOperand.Equals(rightOperand);
        }
    }
}
