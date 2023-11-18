using System.Timers;
using TrionLibrary;

namespace TrionControlPanel.Pages.admin
{
    public partial class Index
    {
        System.Timers.Timer TimerWacher;
        double MachineMaxRam;
        double MachineCurrentRam;
        string MachineRamProcent;
        string MachineCpuUsage;
        int calculate;
        string WorldColor = "Red";
        string LoginColor = "Red";
        string MySQLColor = "Red";

        protected override void OnAfterRender(bool firstRender)
        {
            if (firstRender)
            {
                StartTimers();
            }
            base.OnAfterRender(firstRender);
        }
        private async void OnTimeEvent(Object sorce, ElapsedEventArgs e)
        {
            MachineCpuUsage = SystemWatcher.MachineCpuUtilization().ToString()+"%";
            MachineMaxRam = SystemWatcher.TotalRam();
            MachineCurrentRam = SystemWatcher.CurentPcRamUsage();
            calculate = 100 - Convert.ToInt32(MachineCurrentRam / MachineMaxRam * (long)100);
            MachineRamProcent = calculate.ToString() + "%";
            if (SystemWatcher.ApplicationRuning("fdm") == EnumModels.ServerStatus.Running)
            {
                WorldColor = "Green";
                Settings.Server.WorldServerStatus = EnumModels.ServerStatus.Running;
            }
            else
            {
                WorldColor = "Red";
                Settings.Server.WorldServerStatus = EnumModels.ServerStatus.NotRunning;
            }
            await InvokeAsync(() => StateHasChanged());
        }
        private void StartTimers()
        {
            TimerWacher = new (1000);
            TimerWacher.Elapsed += OnTimeEvent;
            TimerWacher.Enabled = true;
            TimerWacher.Start();   
        }
        protected override async Task OnInitializedAsync()
        {
          StartTimers();
        }
        public void Dispose()
        {
            // While navigating to other components, Dispose method will be called and clean up the Timer function.
            TimerWacher?.Dispose();
        }
    }
}