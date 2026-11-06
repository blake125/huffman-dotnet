using Microsoft.ApplicationInsights.Extensibility.Implementation;
using Xunit.Runner.Common;

namespace Huffman.Core.Tests
{
    public class HuffmanCoderTests
    {
        [Fact]
        public void RoundTripTest()
        {
            MemoryStream stream = new MemoryStream("aaaabbbabab"u8.ToArray());

            MemoryStream encodedStream = new MemoryStream();
            using (var s = HuffmanCoder.Encode(stream) as MemoryStream)
            {
                s.CopyTo(encodedStream);
                encodedStream.Position = 0;
            }

            MemoryStream decodedStream = new MemoryStream();
            using (var s = HuffmanCoder.Decode(encodedStream) as MemoryStream)
            {
                s.CopyTo(decodedStream);
                decodedStream.Position = 0;
            }

            Assert.Equal(stream.ToArray(), decodedStream.ToArray());
        }

        [Fact]
        public void RoundTripButBiggerTest()
        {
            MemoryStream stream = new MemoryStream(
                "ababacwoicjowqheoiqwnjwhoijalsk?????jswofoqjio¥w"u8.ToArray()
                );

            MemoryStream encodedStream = new MemoryStream();
            using (var s = HuffmanCoder.Encode(stream) as MemoryStream)
            {
                s.CopyTo(encodedStream);
                encodedStream.Position = 0;
            }

            MemoryStream decodedStream = new MemoryStream();
            using (var s = HuffmanCoder.Decode(encodedStream) as MemoryStream)
            {
                s.CopyTo(decodedStream);
                decodedStream.Position = 0;
            }

            Assert.Equal(stream.ToArray(), decodedStream.ToArray());
        }

        [Fact]
        public void RoundTripOneCharacterTest()
        {
            MemoryStream stream = new MemoryStream(
                "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa"u8.ToArray()
                );

            MemoryStream encodedStream = new MemoryStream();
            using (var s = HuffmanCoder.Encode(stream) as MemoryStream)
            {
                s.CopyTo(encodedStream);
                encodedStream.Position = 0;
            }

            MemoryStream decodedStream = new MemoryStream();
            using (var s = HuffmanCoder.Decode(encodedStream) as MemoryStream)
            {
                s.CopyTo(decodedStream);
                decodedStream.Position = 0;
            }

            Assert.Equal(stream.ToArray(), decodedStream.ToArray());
        }
    }
}
