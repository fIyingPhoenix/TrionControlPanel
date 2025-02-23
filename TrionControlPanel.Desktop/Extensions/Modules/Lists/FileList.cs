
namespace TrionControlPanel.Desktop.Extensions.Modules.Lists
{
    public class FileList
    {
        public string Name { get; set; }
        public double Size { get; set; }
        public string Hash { get; set; }
        public string Path { get; set; }
    }
    public class FileResponse
    {
        public int Count { get; set; }
        public List<FileList> Files { get; set; } = new();
    }
}
