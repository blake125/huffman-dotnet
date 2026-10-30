namespace Huffman.Core.Tests
{
    public class HuffmanTreeTests
    {
        [Fact]
        public void EmptyFrequencyTableTest()
        {
            Stream stream = new MemoryStream();
            FrequencyTable table = FrequencyTable.FromStream(stream);
            HuffmanTree? tree = HuffmanTree.FromFrequencyTable(table);

            Assert.Null(tree);
        }

        [Fact]
        public void SingleSymbolTest()
        {
            Stream stream = new MemoryStream("aaaa"u8.ToArray());
            FrequencyTable table = FrequencyTable.FromStream(stream);
            HuffmanTree? tree = HuffmanTree.FromFrequencyTable(table);

            Assert.NotNull(tree);
            Assert.NotNull(tree.Root);
            Assert.True(tree.Root.IsLeaf);
            Assert.Equal(4, tree.Root.Weight);
            Assert.Equal((byte)'a', tree.Root.Symbol);
        }

        [Fact]
        public void MultiSymbolTest()
        {
            Stream stream = new MemoryStream("aaaabbc"u8.ToArray());
            FrequencyTable table = FrequencyTable.FromStream(stream);
            HuffmanTree? tree = HuffmanTree.FromFrequencyTable(table);

            //Root tests
            Assert.NotNull(tree);
            Assert.NotNull(tree.Root);
            Assert.False(tree.Root.IsLeaf);
            Assert.Equal(7, tree.Root.Weight);

            //Right leaf test
            HuffmanNode right = tree.Root.Right!;
            Assert.NotNull(right);
            Assert.True(right.IsLeaf);
            Assert.Equal((byte)'a', right.Symbol);
            Assert.Equal(4, right.Weight);
            Assert.Null(right.Left);
            Assert.Null(right.Right);

            //Left internal node test
            HuffmanNode left = tree.Root.Left!;
            Assert.NotNull(left);
            Assert.False(left.IsLeaf);
            Assert.Null(left.Symbol);
            Assert.Equal(3, left.Weight);
            Assert.NotNull(left.Left);
            Assert.NotNull(left.Right);

            //Internal node left node test
            HuffmanNode intLeft = left.Left;
            Assert.NotNull(intLeft);
            Assert.True(intLeft.IsLeaf);
            Assert.Equal((byte)'c', intLeft.Symbol);
            Assert.Equal(1, intLeft.Weight);
            Assert.Null(intLeft.Left);
            Assert.Null(intLeft.Right);

            //Internal node right node test
            HuffmanNode intRight = left.Right;
            Assert.NotNull(intRight);
            Assert.True(intRight.IsLeaf);
            Assert.Equal((byte)'b', intRight.Symbol);
            Assert.Equal(2, intRight.Weight);
            Assert.Null(intRight.Left);
            Assert.Null(intRight.Right);
        }
    }
}
