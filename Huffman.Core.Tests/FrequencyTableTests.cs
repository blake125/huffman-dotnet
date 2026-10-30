namespace Huffman.Core.Tests
{
    public class FrequencyTableTests
    {
        [Fact]
        public void EmptyTableTest()
        {
            Stream stream = new MemoryStream();
            FrequencyTable table = FrequencyTable.FromStream(stream);
            
            Assert.Equal(0, table.Frequencies.Sum());
        }

        [Fact]
        public void SmallTableTest()
        {
            Stream stream = new MemoryStream("aab"u8.ToArray());
            FrequencyTable table = FrequencyTable.FromStream(stream);

            Assert.Equal(2, table.Frequencies['a']);
            Assert.Equal(1, table.Frequencies['b']);
            Assert.Equal(3, table.Frequencies.Sum());
        }

        [Fact]
        public void LargeTableTest()
        {
            byte[] data = new byte[5000];
            new Random(67).NextBytes(data);

            Stream stream = new MemoryStream(data);
            FrequencyTable table = FrequencyTable.FromStream(stream);

            Assert.Equal(5000, table.Frequencies.Sum());
        }
    }
}
