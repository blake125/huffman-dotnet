namespace Huffman.Core.Tests
{
    public class HuffmanNodeTests
    {
        [Fact]
        public void LeafNodeTest()
        {
            const long weight = 234000;
            const byte symbol = 255;

            HuffmanNode node = HuffmanNode.LeafNode(symbol, weight);
            Assert.True(node.IsLeaf);
            Assert.Equal(weight, node.Weight);
            Assert.Equal(symbol, node.Symbol);
            Assert.Null(node.Left);
            Assert.Null(node.Right);
        }

        [Fact]
        public void InternalNodeTest()
        {
            const long weight1 = 4;
            const long weight2 = 1;
            const byte symbol1 = 255;
            const byte symbol2 = 1;

            HuffmanNode left = HuffmanNode.LeafNode(symbol1, weight1);
            HuffmanNode right = HuffmanNode.LeafNode(symbol2, weight2);

            HuffmanNode node = HuffmanNode.InternalNode(left, right);
            Assert.False(node.IsLeaf);
            Assert.Equal(5, node.Weight);
            Assert.Null(node.Symbol);
            Assert.Same(left, node.Left);
            Assert.Same(right, node.Right);
        }
    }
}
