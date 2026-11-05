using System.Collections;
using System.Runtime.CompilerServices;

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

        public static HuffmanTree FromHuffmanCodeTable(HuffmanCodeTable table)
        {
            HuffmanTree tree = new HuffmanTree(HuffmanNode.InternalNode(null, null));
            for (int i = 0; i < 256; i++)
            {
                HuffmanCode code = table.Codes[i];
                if(code.Length == 0)
                {
                    continue;
                }

                tree.AddPath(code, (byte)i);
            }

            return tree;
        }

        private void AddPath(HuffmanCode code, byte symbol)
        {
            HuffmanNode walk = Root!;

            ulong path = code.Code;
            int length = code.Length;
            
            while(length > 1)
            {
                int direction = ((int)path >> length - 1) & 1;
                length -= 1;

                if(direction == 0)
                {
                    if(walk.Left == null)
                    {
                        walk.Left = HuffmanNode.InternalNode(null, null);
                        walk = walk.Left;
                    } else
                    {
                        walk = walk.Left;
                    }
                } else
                {
                    if (walk.Right == null)
                    {
                        walk.Right = HuffmanNode.InternalNode(null, null);
                        walk = walk.Right;
                    }
                    else
                    {
                        walk = walk.Right;
                    }
                }
            }

            int dir = ((int)path >> length - 1) & 1;
            if( dir == 0)
            {
                walk.Left = HuffmanNode.LeafNode(symbol, 0);
            } else
            {
                walk.Right = HuffmanNode.LeafNode(symbol, 0);
            }
        }
    }
}
