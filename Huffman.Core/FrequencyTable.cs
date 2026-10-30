using System.Collections.ObjectModel;

namespace Huffman.Core
{
    internal class FrequencyTable
    {
        private readonly long[] frequencies = new long[256];
        public ReadOnlyCollection<long> Frequencies => Array.AsReadOnly(frequencies);

        private FrequencyTable() { }

        public static FrequencyTable FromStream(Stream stream)
        {
            FrequencyTable table = new FrequencyTable();

            byte[] buffer = new byte[4096];
            int bytesRead = stream.Read(buffer);
            while (bytesRead > 0)
            {
                for (int i = 0; i < bytesRead; i++)
                {
                    table.frequencies[buffer[i]]++;
                }

                bytesRead = stream.Read(buffer);
            }

            return table;
        }
    }
}
