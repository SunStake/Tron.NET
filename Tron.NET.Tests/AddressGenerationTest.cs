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
    }
}
