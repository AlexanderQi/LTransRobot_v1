using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using log4net;
using System.IO;
using log4net.Config;

namespace LTransRobot_v1
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        private ILog log;
        private void InitLog()
        {
            var logfile = new FileInfo(AppDomain.CurrentDomain.BaseDirectory + "Log4net.config");
            XmlConfigurator.ConfigureAndWatch(logfile);
            log = LogManager.GetLogger("log");
        }

        //注意：要在APP.XMAL中绑定该事件处理函数
        private void Application_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            log.Error(e.Exception);
            MessageBox.Show(e.Exception.ToString(), "App error occurred.");
        }

        //注意：要在APP.XMAL中绑定该事件处理函数
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            InitLog();
        }

  
    }
}
