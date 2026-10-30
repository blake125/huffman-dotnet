namespace Huffman.Core
{
    internal class HuffmanNode
    {
        public bool IsLeaf { get; }
        public byte? Symbol {  get; }
        public long Weight { get; }
        public HuffmanNode? Left { get; }
        public HuffmanNode? Right { get; }

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

        public static HuffmanNode InternalNode(HuffmanNode left, HuffmanNode right)
        {
            return new HuffmanNode(false, null, (left.Weight + right.Weight), left, right);
        }
    }
}
