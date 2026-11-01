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
            HuffmanCodeTable table = new HuffmanCodeTable();
            byte[] lengths = new byte[256];

            if (tree.Root!.IsLeaf)
            {
                lengths[(int)tree.Root.Symbol!] = 1;
            } else
            {
                table.TraverseTree(tree.Root!, 0, lengths);
            }

            table.Canonize(lengths);
            
            return table;
        }

        private void TraverseTree(HuffmanNode node, byte length, byte[] lengths)
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
    }
}
