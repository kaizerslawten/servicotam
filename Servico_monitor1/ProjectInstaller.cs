using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Linq;
using System.Management;
using System.Threading.Tasks;


namespace Servico_monitor1
{
    [RunInstaller(true)]
    public partial class ProjectInstaller : System.Configuration.Install.Installer
    {
        public ProjectInstaller()
        {
            InitializeComponent();
        }

        private void serviceProcessInstaller1_AfterInstall(object sender, InstallEventArgs e)
        {
            //this.serviceProcessInstaller1.Account = System.ServiceProcess.ServiceAccount.LocalSystem;
            //this.serviceProcessInstaller1.Username = "antonio.rosario@tivit.com.br";
            //this.serviceProcessInstaller1.Username = "dmtlab\antonio";
            //this.serviceProcessInstaller1.Password = "Tivit@123";

        }

        private void serviceInstaller1_AfterInstall(object sender, InstallEventArgs e)
        {
        }

        private void serviceProcessInstaller1_Committed(object sender, InstallEventArgs e)
        {
            /*ConnectionOptions coOptions = new ConnectionOptions();
            coOptions.Impersonation = ImpersonationLevel.Impersonate;
            ManagementScope mgmtScope = new ManagementScope(@"root\CIMV2", coOptions);
            mgmtScope.Connect();
            ManagementObject wmiService;
            wmiService = new ManagementObject("Win32_Service.Name='" + serviceInstaller1.ServiceName + "'");
            ManagementBaseObject InParam = wmiService.GetMethodParameters("Change");
            InParam["DesktopInteract"] = true;
            ManagementBaseObject OutParam = wmiService.InvokeMethod("Change", InParam, null);*/
        }

        private void serviceInstaller1_Committed(object sender, InstallEventArgs e)
        {
            ConnectionOptions coOptions = new ConnectionOptions();
            coOptions.Impersonation = ImpersonationLevel.Impersonate;
            ManagementScope mgmtScope = new ManagementScope(@"root\CIMV2", coOptions);
            mgmtScope.Connect();
            ManagementObject wmiService;
            wmiService = new ManagementObject("Win32_Service.Name='" + serviceInstaller1.ServiceName + "'");
            ManagementBaseObject InParam = wmiService.GetMethodParameters("Change");
            InParam["DesktopInteract"] = true;
            ManagementBaseObject OutParam = wmiService.InvokeMethod("Change", InParam, null);
        }


        private void serviceInstaller1_AfterInstall_1(object sender, InstallEventArgs e)
        {

        }

        private void ProjectInstaller_BeforeInstall(object sender, InstallEventArgs e)
        {

        }

        public string GetContextParameter(string key)
        {
            string sValue = "";
            try
            {
                sValue = this.Context.Parameters[key].ToString();
            }
            catch
            {
                sValue = "";
            }
            return sValue;
        }

        // Override the 'OnBeforeInstall' method.
        protected override void OnBeforeInstall(IDictionary savedState)
        {
            base.OnBeforeInstall(savedState);

            string username = GetContextParameter("user").Trim();
            string password = GetContextParameter("password").Trim();

            if (username != "")
                serviceProcessInstaller1.Username = username;
            if (password != "")
                serviceProcessInstaller1.Password = password;
        }

    }
}
