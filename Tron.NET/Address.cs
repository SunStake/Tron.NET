using System;
using NBitcoin.DataEncoders;

namespace Tron
{
    public readonly struct Address
    {
        private readonly byte[] addressBytes;
        private readonly string addressWif;

        private const int ADDRESS_LENGTH_IN_BYTES = 20;
        private const byte BASE58CHECK_PREFIX = 0x41;

        // This class should be thread-safe (not officially documented but based on source)
        private static readonly Base58CheckEncoder base58CheckEncoder = new Base58CheckEncoder();

        public Address(ReadOnlySpan<byte> addressBytes)
        {
            if (addressBytes.Length != ADDRESS_LENGTH_IN_BYTES)
                throw new ArgumentException($"Address bytes must be {ADDRESS_LENGTH_IN_BYTES} in length", nameof(addressBytes));

            this.addressBytes = addressBytes.ToArray();

            byte[] base58Buffer = new byte[ADDRESS_LENGTH_IN_BYTES + 1];
            base58Buffer[0] = BASE58CHECK_PREFIX;
            Array.Copy(this.addressBytes, 0, base58Buffer, 1, ADDRESS_LENGTH_IN_BYTES);
            this.addressWif = base58CheckEncoder.EncodeData(base58Buffer);
        }

        public override string ToString()
        {
            return addressWif;
        }
    }
}
