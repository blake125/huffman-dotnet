namespace Huffman.Core
{
    internal class HuffmanTree
    {
        public HuffmanNode? Root { get; }

        private HuffmanTree(HuffmanNode root)
        {
            Root = root;
        }

        public static HuffmanTree? FromFrequencyTable(FrequencyTable table)
        {
            if(table.Frequencies.Sum() == 0)
            {
                return null;
            }

            PriorityQueue<HuffmanNode, long> queue = new PriorityQueue<HuffmanNode, long>();

            //Create a leaf node for each symbol and add it to the priority queue.
            for (int i = 0; i < 256; i++)
            {
                if (table.Frequencies[i] == 0)
                {
                    continue;
                }

                HuffmanNode node = HuffmanNode.LeafNode((byte)i, table.Frequencies[i]);
                queue.Enqueue(node, node.Weight);
            }

            //While there is more than one node in the queue:
            while (queue.Count > 1)
            {
                //Remove the two nodes of highest priority(lowest probability) from the queue
                HuffmanNode left = queue.Dequeue();
                HuffmanNode right = queue.Dequeue();

                //Create a new internal node with these two nodes as children and with probability equal to the sum of the two nodes' probabilities.
                HuffmanNode internalNode = HuffmanNode.InternalNode(left, right);

                //Add the new node to the queue.
                queue.Enqueue(internalNode, internalNode.Weight);
            }

            //The remaining node is the root node and the tree is complete.
            HuffmanNode root = queue.Dequeue();

            return new HuffmanTree(root);
        }

    }
}
