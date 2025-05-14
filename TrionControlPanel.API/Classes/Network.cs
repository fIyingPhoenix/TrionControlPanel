namespace TrionControlPanel.API.Classes
{
    public class Network
    {
        private const int BufferSize = 1024 * 1024; // 1 MB chunks
        public static Stream GenerateRandomStream()
        {
            var stream = new MemoryStream();
            var buffer = new byte[BufferSize];
            new Random().NextBytes(buffer);

            for (int i = 0; i < int.MaxValue; i++) // Infinite stream
            {
                stream.Write(buffer, 0, buffer.Length);
            }

            stream.Position = 0;
            return stream;
        }
    }
}
