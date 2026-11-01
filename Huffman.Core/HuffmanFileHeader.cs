using System.Collections.ObjectModel;

namespace Huffman.Core
{
    internal class HuffmanFileHeader
    {
        private readonly byte[] magicNumber = "HUFF"u8.ToArray();
        private long length;
        private byte[] codeLengths;

        public HuffmanFileHeader(long length, byte[] codeLengths)
        {
            this.length = length;
            this.codeLengths = codeLengths.ToArray();
        }

        public void WriteToStream(Stream s)
        {
            BinaryWriter writer = new BinaryWriter(s);
            writer.Write(magicNumber);
            writer.Write(length);
            writer.Write(codeLengths);
        }

        public static HuffmanFileHeader FromStream(Stream s)
        {
            BinaryReader reader = new BinaryReader(s);

            byte[] magicNumber = reader.ReadBytes(4);

            bool equal = magicNumber.SequenceEqual("HUFF"u8);
            if(!equal)
            {
                throw new Exception("File format not recognized!");
            }

            long length = reader.ReadInt64();
            byte[] codeLengths = reader.ReadBytes(256);

            if(length < 0)
            {
                throw new Exception("Invalid length!");
            }

            if(codeLengths.Length != 256)
            {
                throw new Exception("Malformed file header!");
            }

            if(codeLengths.Max() > 64)
            {
                throw new Exception("Malformed code length!");
            }

            return new HuffmanFileHeader(length, codeLengths);
        }
        public long Length => length;
        public ReadOnlyCollection<byte> CodeLengths => Array.AsReadOnly(codeLengths);
    }
}
