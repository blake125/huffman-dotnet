namespace Huffman.Core.Tests
{
    public class BitStreamWriterTests
    {
        [Fact]
        public void EmptyBitsTest()
        {
            Stream s = new MemoryStream();
            BitStreamWriter writer = new BitStreamWriter(s);
            writer.WriteBits(0, 0);

            Assert.Equal(0, s.Length);
        }

        [Fact]
        public void WriteBitTest()
        {
            MemoryStream s = new MemoryStream();
            BitStreamWriter writer = new BitStreamWriter(s);

            ulong code = 0b1;
            int length = 1;
            
            writer.WriteBits(code, length);
            writer.Pad();

            byte[] data = s.ToArray();

            Assert.Equal(new byte[] { 0b1000_0000 }, data);
        }

        [Fact]
        public void WriteBitsTest()
        {
            MemoryStream s = new MemoryStream();
            BitStreamWriter writer = new BitStreamWriter(s);

            writer.WriteBits(0b10, 2);
            writer.WriteBits(0b011, 3);
            writer.Pad();

            byte[] data = s.ToArray();

            Assert.Equal(new byte[] { 0b1001_1000 }, data);
        }
    }
}
