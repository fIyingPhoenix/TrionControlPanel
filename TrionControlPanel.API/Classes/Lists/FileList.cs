namespace TrionControlPanel.API.Classes.Lists
{
    public class FileList
    {
        public string Name { get; set; }
        public double Size { get; set; }
        public string Hash { get; set; }
        public string Path { get; set; }
    }


    // Define a DTO class
    public class FileRequest
    {
        public string FilePath { get; set; }
    }
}
