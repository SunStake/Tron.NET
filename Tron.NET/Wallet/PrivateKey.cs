using System;
using Tron.Comparers;

namespace Tron.Wallet
{
    public readonly struct PrivateKey : IEquatable<PrivateKey>
    {
        private readonly byte[] data;
        private readonly int hashCode;

        private const int KEY_LENGTH_IN_BYTES = 32;

        public PrivateKey(byte[] data, int offset = 0)
        {
            if (data.Length < offset + KEY_LENGTH_IN_BYTES)
                throw new ArgumentException($"Length of {nameof(data)} must be at least {offset + KEY_LENGTH_IN_BYTES} bytes", nameof(data));

            this.data = new byte[KEY_LENGTH_IN_BYTES];
            Array.Copy(data, 0, this.data, offset, KEY_LENGTH_IN_BYTES);

            hashCode = ByteArrayEqualityComparer.Instance.GetHashCode(this.data);
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
