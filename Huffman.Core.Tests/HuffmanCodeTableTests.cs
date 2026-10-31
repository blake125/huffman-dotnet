using Xunit.Sdk;

namespace Huffman.Core.Tests
{
    public class HuffmanCodeTableTests
    {
        [Fact]
        public void SingleSymbolTest()
        {
            Stream stream = new MemoryStream("aaaa"u8.ToArray());
            FrequencyTable table = FrequencyTable.FromStream(stream);
            HuffmanTree? tree = HuffmanTree.FromFrequencyTable(table);
            HuffmanCodeTable huffTable = HuffmanCodeTable.FromHuffmanTree(tree!);

            Assert.Equal((ulong)0, huffTable.Codes[(byte)'a'].Code);
            Assert.Equal(1, huffTable.Codes[(byte)'a'].Length);
        }

        [Fact]
        public void MultiSymbolTest()
        {
            Stream stream = new MemoryStream("aaaabbc"u8.ToArray());
            FrequencyTable table = FrequencyTable.FromStream(stream);
            HuffmanTree? tree = HuffmanTree.FromFrequencyTable(table);
            HuffmanCodeTable huffTable = HuffmanCodeTable.FromHuffmanTree(tree!);

            Assert.Equal((ulong)0b00, huffTable.Codes[(byte)'c'].Code);
            Assert.Equal(2, huffTable.Codes[(byte)'c'].Length);
            
            Assert.Equal((ulong)0b01, huffTable.Codes[(byte)'b'].Code);
            Assert.Equal(2, huffTable.Codes[(byte)'b'].Length);
            
            Assert.Equal((ulong)1, huffTable.Codes[(byte)'a'].Code);
            Assert.Equal(1, huffTable.Codes[(byte)'a'].Length);
        }
    }
}
