using TrionLibrary;
namespace TrionWorker
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("World Status: " + SystemWatcher.WorldRuning().ToString());
            RunOnStart();
        }
        private static void RunOnStart()
        {
            TCPRunCheck();
        }
        private static void TCPRunCheck()
        {
            if (SystemWatcher.TCPRuning() == false)
            {
                System.Diagnostics.Process.Start(SystemWatcher.TCPName);
            }
        }
    }
}