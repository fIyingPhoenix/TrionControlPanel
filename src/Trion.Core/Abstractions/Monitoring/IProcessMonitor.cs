namespace Trion.Core.Abstractions.Monitoring;

public interface IProcessMonitor
{
    void Track(int pid);
    void Untrack(int pid);
    event Action<int>? OnProcessExited;
}
