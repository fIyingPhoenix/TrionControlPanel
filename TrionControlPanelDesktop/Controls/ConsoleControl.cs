using MetroFramework.Controls;
using Microsoft.Scripting.Hosting.Shell;
using System.Diagnostics;
using System.Xml;
using TrionLibrary;

namespace TrionControlPanelDesktop.Controls
{
    public partial class ConsoleControl : UserControl
    {
        private List<Tuple<Process, RichTextBox>> _processes;
        //public static bool RunDatabase { get; set; }
        //public static bool RunWorld { get; set; }
        //public static bool RunLogon { get; set; }
        //public static string Arguments { get; set; }
        public ConsoleControl()
        {
            Dock = DockStyle.Fill;
            InitializeComponent();
            _processes = [];
        }
        private void StartServer(string Name, string Application, string Arguments)
        {

            // Start the console application process
            Process process = new();
            process.StartInfo.FileName = Application;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.RedirectStandardInput = true;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.CreateNoWindow = true;
            if (Arguments != null && Application.Contains(Data.Settings.DBExecutableName))
            {
                process.StartInfo.Arguments = Arguments;
            }
            // Create a new RichTextBox for this process
            RichTextBox richTextBox = new()
            {
                Dock = DockStyle.Fill,
                BorderStyle = BorderStyle.None,
                Font = new Font("Segoe UI Semibold", 10, FontStyle.Bold),
                ReadOnly = true,
                ForeColor = Color.White,
                BackColor = Color.FromArgb(34, 39, 46)  
            };

            // Add the RichTextBox to the form
            MetroTabPage tabPage = new()
            {
                Text = Name,
                BackColor = Color.FromArgb(34, 39, 46), 
                Padding = new Padding(2, 2,2,2)
            };
            tabPage.Controls.Add(richTextBox);
            TabControl1.TabPages.Add(tabPage);

            // Event handler for capturing console output
            process.OutputDataReceived += (sender, args) =>
            {
                if (!string.IsNullOrEmpty(args.Data))
                {
                    // Update the RichTextBox with the console output
                    UpdateRichTextBox(richTextBox, args.Data + Environment.NewLine);
                }
            };

            // Start the process
            process.Start();
            process.BeginOutputReadLine();

            // Add the process and its associated RichTextBox to the list
            _processes.Add(new Tuple<Process, RichTextBox>(process, richTextBox));
        }
        private void UpdateRichTextBox(RichTextBox richTextBox, string text)
        {
            if (richTextBox.InvokeRequired)
            {
                richTextBox.BeginInvoke(new Action(() => UpdateRichTextBox(richTextBox, text)));
            }
            else
            {
                richTextBox.AppendText(text);
            }
        }
        private void TimerWacher_Tick(object sender, EventArgs e)
        {
            //if (RunDatabase)
            //{
            //    RunDatabase = false;
            //    StartServer("Database", Data.Settings.MySQLExecutableLocation, Arguments);
            //    MessageBox.Show(Data.Settings.MySQLExecutableLocation);
            //}
            //if (RunWorld)
            //{
            //    RunWorld = false;
            //    MessageBox.Show(Data.Settings.WorldExecutableLocation);
            //    StartServer("World", Data.Settings.WorldExecutableLocation, null!);
            //}
            //if (RunLogon)
            //{
            //    RunLogon = false;
            //    StartServer("Logon", Data.Settings.LogonExecutableLocation, null!);
            //}
        }
    }
}
