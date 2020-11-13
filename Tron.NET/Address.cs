using System;
using NBitcoin.DataEncoders;
using Tron.Utilities;

namespace Tron
{
    public readonly struct Address
    {
        private readonly byte[] addressBytes;

        private const int ADDRESS_LENGTH_IN_BYTES = 20 + 1;
        private const byte BASE58CHECK_PREFIX = 0x41;

        // This class should be thread-safe (not officially documented but based on source)
        private static readonly Base58CheckEncoder base58CheckEncoder = new Base58CheckEncoder();

        public Address(ReadOnlySpan<byte> addressBytes)
        {
            if (addressBytes.Length == ADDRESS_LENGTH_IN_BYTES)
            {
                // Prefix included
                if (addressBytes[0] != BASE58CHECK_PREFIX)
                    throw new ArgumentException("Invalid address prefix", nameof(addressBytes));

                this.addressBytes = addressBytes.ToArray();
            }
            else if (addressBytes.Length == ADDRESS_LENGTH_IN_BYTES - 1)
            {
                // Prefix not included
                this.addressBytes = new byte[ADDRESS_LENGTH_IN_BYTES];
                this.addressBytes[0] = BASE58CHECK_PREFIX;
                addressBytes.CopyTo(new Span<byte>(this.addressBytes, 1, ADDRESS_LENGTH_IN_BYTES - 1));
            }
            else
            {
                throw new ArgumentException("Invalid address bytes length", nameof(addressBytes));
            }
        }

        public static Address FromBase58(string base58Address)
        {
            byte[] decodedBytes = base58CheckEncoder.DecodeData(base58Address);
            if (decodedBytes.Length != ADDRESS_LENGTH_IN_BYTES)
                throw new ArgumentException("Invalid address format", nameof(base58Address));

            return new Address(decodedBytes);
        }

        public string ToHexString(bool hexPrefix = true)
        {
            return HexUtilities.BytesToHex(addressBytes, hexPrefix);
        }

        public override string ToString()
        {
            byte[] base58Buffer = new byte[ADDRESS_LENGTH_IN_BYTES + 1];
            base58Buffer[0] = BASE58CHECK_PREFIX;
            Array.Copy(this.addressBytes, 0, base58Buffer, 1, ADDRESS_LENGTH_IN_BYTES);

            return base58CheckEncoder.EncodeData(addressBytes);
        }
    }
}
