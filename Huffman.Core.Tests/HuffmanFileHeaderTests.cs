namespace Huffman.Core.Tests
{
    public class HuffmanFileHeaderTests
    {
        [Fact]
        public void FileHeaderTest()
        {
            byte[] codeLengths = new byte[256];
            codeLengths[(byte)'a'] = 1;
            codeLengths[(byte)'b'] = 4;
            codeLengths[(byte)'z'] = 2;
            HuffmanFileHeader header = new HuffmanFileHeader(7, codeLengths);

            MemoryStream stream = new MemoryStream();
            header.WriteToStream(stream);

            stream.Position = 0;
            HuffmanFileHeader decodedHeader = HuffmanFileHeader.FromStream(stream);

            Assert.Equal(7, decodedHeader.Length);
            Assert.Equal(codeLengths, decodedHeader.CodeLengths);
        }

        [Fact]
        public void InvalidMagicNumberTest()
        {
            MemoryStream s = new MemoryStream();
            s.Write("NOTHUFF"u8.ToArray());
            s.Position = 0;

            
            Assert.Throws<Exception>(() => HuffmanFileHeader.FromStream(s));
        }
    }
}
