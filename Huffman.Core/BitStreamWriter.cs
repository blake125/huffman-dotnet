namespace Huffman.Core
{
    internal class BitStreamWriter
    {
        private byte data = 0;
        private int bitsUsed = 0;
        private readonly Stream stream;

        public BitStreamWriter(Stream stream)
        {
            this.stream = stream;
        }

        public void WriteBits(ulong bits, int count)
        {
            for(int i = count - 1; i >= 0; i--)
            {
                byte bit = (byte)((bits >> i) & 1);
                data = (byte)((data << 1) | bit);
                bitsUsed++;

                if(bitsUsed == 8)
                {
                    stream.WriteByte(data);
                    bitsUsed = 0;
                    data = 0;
                }
            }
        }

        public int Pad()
        {
            if(bitsUsed == 0)
            {
                return 0;
            }

            int padSize = 8 - bitsUsed;
            data = (byte)(data << padSize);
            stream.WriteByte(data);
            bitsUsed = 0;
            data = 0;

            return padSize;
        }
    }
}
