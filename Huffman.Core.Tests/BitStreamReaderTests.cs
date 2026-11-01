namespace Huffman.Core.Tests
{
    public class BitStreamReaderTests
    {
        [Fact]
        public void EmptyStreamTest()
        {
            Stream s = new MemoryStream();
            BitStreamReader reader = new BitStreamReader(s);

            int bit = reader.ReadBit();

            Assert.Equal(-1, bit);
        }

        [Fact]
        public void BitStreamTest()
        {
            Stream s = new MemoryStream([0b1011_0000]);
            BitStreamReader reader = new BitStreamReader(s);

            int bit1 = reader.ReadBit();
            int bit2 = reader.ReadBit();
            int bit3 = reader.ReadBit();
            int bit4 = reader.ReadBit();

            Assert.Equal(1, bit1);
            Assert.Equal(0, bit2);
            Assert.Equal(1, bit3);
            Assert.Equal(1, bit4);
        }
    }
}
