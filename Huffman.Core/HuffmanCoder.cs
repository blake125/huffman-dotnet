using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Runtime.CompilerServices;
using System.Text;

namespace Huffman.Core
{
    internal class HuffmanCoder
    {
        public static Stream Encode(Stream stream)
        {
            MemoryStream encodedStream = new MemoryStream();

            FrequencyTable table = FrequencyTable.FromStream(stream);
            HuffmanTree tree = HuffmanTree.FromFrequencyTable(table);
            HuffmanCodeTable codeTable = HuffmanCodeTable.FromHuffmanTree(tree);

            long streamLength = stream.Length;
            HuffmanFileHeader header = new HuffmanFileHeader(streamLength, codeTable.GetCodeLengths());
            header.WriteToStream(encodedStream);

            BitStreamWriter writer = new BitStreamWriter(encodedStream);

            stream.Position = 0;
            int data = stream.ReadByte();
            while (data != -1)
            {
                HuffmanCode code = codeTable.Codes[data];
                writer.WriteBits(code.Code, code.Length);

                data = stream.ReadByte();
            }

            writer.Pad();

            encodedStream.Position = 0;
            return encodedStream;
        }

        public static Stream Decode(Stream s)
        {
            MemoryStream decodedStream = new MemoryStream();

            HuffmanFileHeader header = HuffmanFileHeader.FromStream(s);
            HuffmanCodeTable table = HuffmanCodeTable.FromCodeLengths(header.CodeLengths.ToArray());
            HuffmanTree tree = HuffmanTree.FromHuffmanCodeTable(table);
            long length = header.Length;

            BitStreamReader reader = new BitStreamReader(s);

            long read = 0;
            HuffmanNode walk = tree.Root;
            while (read < length)
            {
                int bit = reader.ReadBit();
                if (bit != -1)
                {
                    if (bit == 0)
                    {
                        walk = walk.Left;
                    }
                    else
                    {
                        walk = walk.Right;
                    }

                    if (walk.IsLeaf)
                    {
                        decodedStream.WriteByte(walk.Symbol.Value);
                        read++;
                        walk = tree.Root;
                    }
                }
            }

            decodedStream.Position = 0;
            return decodedStream;
        }
    }
}
