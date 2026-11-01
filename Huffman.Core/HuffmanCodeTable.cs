using System.Collections.ObjectModel;

namespace Huffman.Core
{
    internal class HuffmanCodeTable
    {
        private HuffmanCode[] codes = new HuffmanCode[256];
        public ReadOnlyCollection<HuffmanCode> Codes => Array.AsReadOnly<HuffmanCode>(codes);

        private HuffmanCodeTable() { }

        public static HuffmanCodeTable FromHuffmanTree(HuffmanTree tree)
        {
            byte[] lengths = new byte[256];

            if (tree.Root!.IsLeaf)
            {
                lengths[(int)tree.Root.Symbol!] = 1;
            } else
            {
                TraverseTree(tree.Root!, 0, lengths);
            }

            return FromCodeLengths(lengths);
        }

        private static void TraverseTree(HuffmanNode node, byte length, byte[] lengths)
        {
            if(node ==  null) return;

            if (node.IsLeaf)
            {
                lengths[(byte)node.Symbol!] = length;
                return;
            }

            length++;
            TraverseTree(node.Left!, length, lengths);
            TraverseTree(node.Right!, length, lengths);
        }

        private void Canonize(byte[] lengths)
        {
            ulong code = 0;
            int maxLength = lengths.Max();
            for (int i = 1; i <= maxLength; i++)
            {
                for (int j = 0; j < 256; j++)
                {
                    if (lengths[j] == i)
                    {
                        HuffmanCode c = new HuffmanCode(code, i);
                        codes[j] = c;

                        code++;
                    }
                }

                code = code << 1;
            }
        }

        public static HuffmanCodeTable FromCodeLengths(byte[] codeLengths)
        {
            HuffmanCodeTable table = new HuffmanCodeTable();
            table.Canonize(codeLengths);

            return table;
        }
    }
}
