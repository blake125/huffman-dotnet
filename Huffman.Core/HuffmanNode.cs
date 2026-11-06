namespace Huffman.Core
{
    internal class HuffmanNode
    {
        public bool IsLeaf { get; }
        public byte? Symbol {  get; }
        public long Weight { get; }
        public HuffmanNode? Left { get; set; }
        public HuffmanNode? Right { get; set; }

        private HuffmanNode(bool isLeaf, byte? symbol, long weight, HuffmanNode? left, HuffmanNode? right) 
        {
            IsLeaf = isLeaf;
            Symbol = symbol;
            Weight = weight;
            Left = left;
            Right = right;
        }

        public static HuffmanNode LeafNode(byte symbol, long weight)
        {
            return new HuffmanNode(true, symbol, weight, null, null);
        }

        public static HuffmanNode InternalNode(HuffmanNode? left, HuffmanNode? right)
        {
            if(left == null || right == null)
            {
                return new HuffmanNode(false, null, 0, left, right);
            }

            return new HuffmanNode(false, null, (left.Weight + right.Weight), left, right);
        }
    }
}
