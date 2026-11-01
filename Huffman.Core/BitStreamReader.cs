using System;
using System.Collections.Generic;
using System.Text;

namespace Huffman.Core
{
    internal class BitStreamReader
    {
        private readonly Stream stream;
        private int data = 0;
        private int bitsRead = 0;

        public BitStreamReader(Stream s)
        {
            stream = s;
        }

        public int ReadBit()
        {
            if (bitsRead == 0)
            {
                data = stream.ReadByte();
            }

            if(data == -1)
            {
                return data;
            }

            int bit = data >> (7 - bitsRead) & 1;
            bitsRead++;

            if (bitsRead == 8)
            {
                bitsRead = 0;
            }

            return bit;
        }
    }
}
