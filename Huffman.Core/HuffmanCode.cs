namespace Huffman.Core
{
    internal struct HuffmanCode
    {
        public ulong Code { get; }
        public int Length { get; }

        public HuffmanCode(ulong code, int length)
        {
            Code = code;
            Length = length;
        }
    }
}
