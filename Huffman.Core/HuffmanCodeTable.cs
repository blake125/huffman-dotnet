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

            if (tree.Root!.IsLeaf)
            {
                HuffmanCode huffCode = new HuffmanCode(0, 1);
                table.codes[(byte)tree.Root.Symbol!] = huffCode;
            } else
            {
                table.TraverseTree(tree.Root!, 0, 0);
            }
            
            return table;
        }

        private void TraverseTree(HuffmanNode node, ulong code, int length)
        {
            if(node ==  null) return;

            if (node.IsLeaf)
            {
                HuffmanCode huffCode = new HuffmanCode(code, length);
                codes[(byte)node.Symbol!] = huffCode;
                return;
            }

            TraverseTree(node.Left!, code << 1, length + 1);
            TraverseTree(node.Right!, (code << 1) | 1, length + 1);
        }

        
    }
}
