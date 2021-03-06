using Xunit;
using Tron.Wallet;
using Tron.Utilities;

namespace Tron.Tests
{
    public class AddressGenerationTest
    {
        // Test data generated from the official JavaScript library tronweb
        [Theory]
        [InlineData("0xb77c5fdfc3b2692f506f86b0d24e88ab78f2529d2deced8256e6f5a525ecf218", "TV6iRCZvpFJU5Z6tb9WK2JDPzEYXUyShq6")]
        [InlineData("0x14d1912bc39ccab645a88816bacc5a92e643a4c9231cfbca8fdc88ddbdc40a63", "TSTTungdsUMXYCSACRt4AFVhnEoX6gokpd")]
        [InlineData("0x7d6d9b01bc5de7dc289c578464d7c88a8b62bee24828f5f8d5c79be713ecf981", "TK7issuVu1t4gdBgwXXjQdnFjcz1WUMK7s")]
        [InlineData("0x9e25994c3059d5895cc7a186c47666b714fe515a06281a5477a22bf5c100c93b", "TCjUZurvybkQVzX7u97YphDktpidMthJVs")]
        [InlineData("0xa2d6e63e3e7b59318d348581866e335f60e3d2149a9f7a760a44d58e2ddfc85b", "TRo1prFYPm4tHR9NTEhtgYLNuye3RgXboC")]
        [InlineData("0xfe66e3ee90277999a51fccd1a299ce096fcc94dd2db4a0035b6f34f9a6cb04ad", "TMgkUbaT8R7HKhFQoMbVFL7mggYBMF6sGh")]
        [InlineData("0x6c2a82208a45ce396a396e7ad2a8d47176cb1e604bea2e49c6e09220f37ff3da", "TEhddV7ovcAsV4JPWsWGs55hSu1w8S34FM")]
        [InlineData("0x1b1f657816ae7e29fe5b73f4452531b39bc283daaca36281fb385aaab3ae46ea", "TFQK44peZkat6TydzHYXxCSmqU89rumHpP")]
        [InlineData("0x46945fb2425b5ac74df562f0763a04a0aedf8530fb182e77bde47cde54d4754f", "TCEx1LevkKY1Rem66Z7dy5JfQLfnYopx8A")]
        [InlineData("0x1589adb0c57e703f68bb8db641720ac134b9fcd42915ee2d66e7f55edf7c7379", "TXGzXJCDt5pW8aNfgH4i6K1LurHydn5V9r")]
        public void PrivateKeyToAddressWifTest(string privateKeyHex, string addressWif)
        {
            Assert.Equal(addressWif, new PrivateKey(HexUtilities.HexToBytes(privateKeyHex)).ToPublicKey().ToAddress().ToString());
        }

        // Test data generated from the official JavaScript library tronweb
        [Theory]
        [InlineData("41d1d5db67fae5b18b31bff69592aa06a78750002d", "TV6iRCZvpFJU5Z6tb9WK2JDPzEYXUyShq6")]
        [InlineData("41b4da1ae041b8310dc56d8699e690ac6086404a74", "TSTTungdsUMXYCSACRt4AFVhnEoX6gokpd")]
        [InlineData("416455662e354b690047cc10071b25ec091e5a7eeb", "TK7issuVu1t4gdBgwXXjQdnFjcz1WUMK7s")]
        [InlineData("411e4f8c4fbab4fc09e961ab53ecaf1fe1c7af4845", "TCjUZurvybkQVzX7u97YphDktpidMthJVs")]
        [InlineData("41ad94860e1a907b55d56873f4d91e5a7f129c94d8", "TRo1prFYPm4tHR9NTEhtgYLNuye3RgXboC")]
        [InlineData("418084b63e54af076c4dbacc1a38e23dacdc900c22", "TMgkUbaT8R7HKhFQoMbVFL7mggYBMF6sGh")]
        [InlineData("4133e6883048dcef68dcbbe72135c450749c66adde", "TEhddV7ovcAsV4JPWsWGs55hSu1w8S34FM")]
        [InlineData("413b98146dbc34e1e26c69264cddab470747f76036", "TFQK44peZkat6TydzHYXxCSmqU89rumHpP")]
        [InlineData("4118ea63be8ddd41228b925391f69bc4d87994847f", "TCEx1LevkKY1Rem66Z7dy5JfQLfnYopx8A")]
        [InlineData("41e9b7b64d656b2fe06800a1eae400f4ad6005fc00", "TXGzXJCDt5pW8aNfgH4i6K1LurHydn5V9r")]
        public void AddressFromBytesTest(string addressHex, string addressWif)
        {
            // With prefix
            Assert.Equal(addressWif, new Address(HexUtilities.HexToBytes(addressHex)).ToString());

            // Without prefix
            Assert.Equal(addressWif, new Address(HexUtilities.HexToBytes(addressHex.Substring(2))).ToString());
        }

        [Theory]
        [InlineData("TV6iRCZvpFJU5Z6tb9WK2JDPzEYXUyShq6")]
        [InlineData("TSTTungdsUMXYCSACRt4AFVhnEoX6gokpd")]
        [InlineData("TK7issuVu1t4gdBgwXXjQdnFjcz1WUMK7s")]
        [InlineData("TCjUZurvybkQVzX7u97YphDktpidMthJVs")]
        [InlineData("TRo1prFYPm4tHR9NTEhtgYLNuye3RgXboC")]
        [InlineData("TMgkUbaT8R7HKhFQoMbVFL7mggYBMF6sGh")]
        [InlineData("TEhddV7ovcAsV4JPWsWGs55hSu1w8S34FM")]
        [InlineData("TFQK44peZkat6TydzHYXxCSmqU89rumHpP")]
        [InlineData("TCEx1LevkKY1Rem66Z7dy5JfQLfnYopx8A")]
        [InlineData("TXGzXJCDt5pW8aNfgH4i6K1LurHydn5V9r")]
        public void AddressFromBase58Test(string base58Address)
        {
            Assert.Equal(base58Address, Address.FromBase58(base58Address).ToString());
        }
    }
}
