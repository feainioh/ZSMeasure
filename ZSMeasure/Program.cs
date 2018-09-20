using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Threading;
using System.Diagnostics;
using System.IO;

namespace ZSMeasure
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            //处理未捕获的异常
            //Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(Application_ThreadException);
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);
            Application.ApplicationExit += new EventHandler(Application_ApplicationExit);

            bool isAppRunning = false;
            Mutex mutex = new Mutex(true, Process.GetCurrentProcess().ProcessName, out isAppRunning);
            if (!isAppRunning)
            {
                MessageBox.Show("程序已经在运行了，请不要重复运行！");
                Environment.Exit(1);
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new MainForm());
            Application.Run(new Welcome());

        }


        static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            //e.ExceptionObject
            try
            {
                Exception ex = (Exception)e.ExceptionObject;
                string errorMsg = "Main非窗体线程异常 : \n\n";
                appendNewLogMessage(errorMsg + ex.ToString() + "\r\n" + ex.Message + Environment.NewLine + ex.StackTrace);
            }
            catch
            {
                appendNewLogMessage("Main不可恢复的非Windows窗体线程异常，应用程序将退出！");
            }
        }

        static void Application_ApplicationExit(object sender, EventArgs e)
        {
            // 可在程序即将退出时做点事情。
            //throw new Exception("The method or operation is not implemented.");
            //writlog("Main异常未被捕获：");
        }

        static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            // e.Exception 可通过判断e的Exception来查找异常。
            try
            {
                string errorMsg = "Main Windows窗体线程异常 : \n\n";
                appendNewLogMessage(errorMsg + e.Exception.ToString() + "\r\n" + e.Exception.Message + Environment.NewLine + e.Exception.StackTrace);
            }
            catch
            {
                appendNewLogMessage("Main不可恢复的Windows窗体异常，应用程序将退出！");
            }
        }


        public static void appendNewLogMessage(string str)
        {
            string time = DateTime.Now.ToString("yyyy-MM-dd");
            try
            {
                string _logfile = Application.StartupPath + "\\LOG\\" + time + ".txt";
                FileStream FS = new FileStream(_logfile, FileMode.Append);
                string str_record = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + "\0\0\0\0" + str;
                StreamWriter SW = new StreamWriter(FS);
                SW.WriteLine(str_record);
                SW.Close();
                SW.Dispose();
            }
            catch { }
        }

    }
}
