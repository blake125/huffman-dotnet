namespace Huffman.Core.Tests
{
    public class BitStreamTests
    {
        [Fact]
        public void StreamTest()
        {
            MemoryStream stream = new MemoryStream();
            BitStreamWriter writer = new BitStreamWriter(stream);

            writer.WriteBits(0b1010_0011, 8);
            writer.Pad();

            stream.Position = 0;

            BitStreamReader reader = new BitStreamReader(stream);
            int[] bits = new int[8];
            for (int i = 0; i < 8; i++)
            {
                bits[i] = reader.ReadBit(); 
            }

            Assert.Equal([1,0,1,0,0,0,1,1], bits);
        }
    }
}
