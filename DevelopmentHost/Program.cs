using System;
using System.Windows.Forms;
using CODE.Framework.Services.Tools.Windows;
using FcsuAgent.Services.Implementation;

namespace Services.DevelopmentHost
{
    static class Program
    {
        /// <summary>
        /// This is a Windows Forms application designed to host your services during development.
        /// This application is not designed for use in production. 
        /// Host your services in a Windows Service project, in IIS, in Azure or in another Windows Forms application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var host = new TestServiceHost();

            //host.AllowHttpCrossDomainCalls();
            //host.AllowSilverlightCrossDomainCalls();

            //host.AddServiceHostBasicHttp(typeof(FcsuServices), true);
            //host.AddServiceHostWsHttp(typeof(FcsuServices), true);
            host.AddServiceHostNetTcp(typeof(FcsuServices));
            //host.AddServiceHostRestJson(typeof(FcsuServices));
            //host.AddServiceHostRestXml(typeof(FcsuServices));

            Application.Run(host);
        }
    }
}