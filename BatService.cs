using System;
using System.Diagnostics;
using System.IO;
using System.ServiceProcess;

namespace Bat4WindowsService
{
    /// <summary>
    /// @author: liusx
    /// @repository: https://github.com/l634666/Bat4WindowsService
    /// </summary>
    partial class BatService : ServiceBase
    {
        /// <summary>
        /// 日志文件
        /// </summary>
        private static string _LogAddress;

        public BatService()
        {
            InitializeComponent();
        }
        protected override void OnStart(string[] args)
        {

            WriteLog("服务启动！");

            try
            {
                silentRunBat(ConfigInfo.StartupBatFilePath);
            }
            catch (System.Exception e)
            {
                WriteExceptionLog(e);
            }
        }

        protected override void OnStop()
        {
            WriteLog("服务停止！");

            try
            {
                silentRunBat(ConfigInfo.ShutdownBatFilePath);
            }
            catch (System.Exception e)
            {
                WriteExceptionLog(e);
            }
        }

        /// <summary>
        /// 静默执行 bat
        /// </summary>
        /// <param name="fullBatFilePath"></param>
        protected void silentRunBat(String fullBatFilePath)
        {
            Process p = new Process();

            if (Path.IsPathRooted(fullBatFilePath))
            {
                p.StartInfo.FileName = fullBatFilePath;
            }
            else
            {
                p.StartInfo.FileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fullBatFilePath);
            }
            p.StartInfo.WorkingDirectory = Path.GetDirectoryName(p.StartInfo.FileName);
            p.StartInfo.CreateNoWindow = true;   //不创建该进程的窗口
            p.StartInfo.UseShellExecute = false;   //不使用shell壳运行
            p.Start();
        }

        /// <summary>
        /// 打印到LOG文件
        /// </summary>
        /// <param name="msg">日志内容</param>
        public static void WriteLog(String content)
        {
            try
            {
                StreamWriter sw = null;
                FileInfo file = new FileInfo(LogFilePath());

                if (!file.Exists)
                {
                    sw = file.CreateText();
                }
                else {
                    sw = file.AppendText();
                }

                //把信息输出到文件
                sw.WriteLine("当前时间：" + DateTime.Now.ToString());
                sw.WriteLine("提示信息：" + content);
                sw.WriteLine();
                sw.Close();
            }
            catch (System.Exception e) { }
        }

        /// <summary>
        /// 将异常打印到LOG文件
        /// </summary>
        /// <param name="ex">异常</param>
        public static void WriteExceptionLog(Exception ex)
        {
            try
            {
                StreamWriter sw = null;
                FileInfo file = new FileInfo(LogFilePath());

                if (!file.Exists)
                {
                    sw = file.CreateText();
                }
                else
                {
                    sw = file.AppendText();
                }

                //把异常信息输出到文件
                sw.WriteLine("当前时间：" + DateTime.Now.ToString());
                sw.WriteLine("异常信息：" + ex.Message);
                sw.WriteLine("异常对象：" + ex.Source);
                sw.WriteLine("调用堆栈：\n" + ex.StackTrace.Trim());
                sw.WriteLine("触发方法：" + ex.TargetSite);
                sw.WriteLine();
                sw.Close();
            }
            catch (System.Exception e) { }
        }

        /// <summary>
        /// 日志文件路径
        /// </summary>
        /// <returns></returns>
        private static string LogFilePath() {

            if (string.IsNullOrEmpty(_LogAddress))
            {
                if (Path.IsPathRooted(ConfigInfo.LogFilePath))
                {
                    _LogAddress = ConfigInfo.LogFilePath;
                }
                else
                {
                    _LogAddress = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ConfigInfo.LogFilePath);
                }
            }

            return _LogAddress;
        }
    }
}
