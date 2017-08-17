using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Principal;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Toolkit;


namespace Servico_monitor1
{
    public partial class Scheduler : ServiceBase
    {
        private Timer timer1 = null;

        public Scheduler()
        {
            InitializeComponent();
        }
        public const int LOGON32_LOGON_INTERACTIVE = 2;
        public const int LOGON32_PROVIDER_DEFAULT = 0;

        WindowsImpersonationContext impersonationContext;


        [DllImport("advapi32.dll")]
        public static extern int LogonUserA(String lpszUserName,
        String lpszDomain,
        String lpszPassword,
        int dwLogonType,
        int dwLogonProvider,
        ref IntPtr phToken);
        [DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern int DuplicateToken(IntPtr hToken,
        int impersonationLevel,
        ref IntPtr hNewToken);

        [DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool RevertToSelf();

        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        public static extern bool CloseHandle(IntPtr handle);


        Process ps = new Process();
        ProcessStartInfo processStartInfo = new ProcessStartInfo();
        uint PID1 = 0;

        protected override void OnStart(string[] args)
        {
            timer1 = new Timer();
            this.timer1.Interval = 5000;
            this.timer1.Elapsed += new System.Timers.ElapsedEventHandler(this.timer1_Tick);
            timer1.Enabled = true;
            //ps = Process.Start(@"c:\tivit\diy\DIY.jar");
            //ProcessStartInfo info = new ProcessStartInfo(@"c:\tivit\diy\DIY.jar");
            //ps.StartInfo.LoadUserProfile = true;
            //ps = Process.Start(@"c:\tivit\diy\DIY.jar");

            //String applicationName = "\"C:\\TIVIT\\DIY\\reset.exe\"";
            String applicationName = "\"C:\\Program_manager\\AgenteTAM.exe\"";

            // launch the application
            //ApplicationLoader.PROCESS_INFORMATION procInfo;
            //ApplicationLoader.StartProcessAndBypassUAC(applicationName, out procInfo);
            ps = Process.Start(@"c:\program_manager\AgenteTAM.exe");
            //ps = Process.GetProcessesByName("AgenteTAM.exe")[0];
            
            // launch the application           
            Library.WriteErrorLog("Servico Agente TAM Iniciado.");
        }




        private void timer1_Tick(object sender, ElapsedEventArgs e)
        {
            //Validando se programa foi encerrado para reiniciar

            if (ps.HasExited)
            {
                //String applicationName = "\"C:\\TIVIT\\DIY\\reset.exe\"";
                //String applicationName = "\"C:\\Program_manager\\AgenteTAM.exe\"";

                //launch the application
                //ApplicationLoader.PROCESS_INFORMATION procInfo;
                //ApplicationLoader.StartProcessAndBypassUAC(applicationName, out procInfo);
                ps = Process.Start(@"c:\program_manager\AgenteTAM.exe");
                //ps = Process.GetProcessesByName("AgenteTAM.exe")[0];
                Library.WriteErrorLog("Aplicativo reiniciado");
            }
        }

        protected override void OnStop()
        {
            timer1.Enabled = false;
            ps.Close();
            Library.WriteErrorLog("Servico Agente TAM parado.");
        }

    }
}
