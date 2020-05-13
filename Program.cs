using System.ServiceProcess;

namespace Bat4WindowsService
{
    /// <summary>
    /// @author: liusx
    /// @repository: https://github.com/l634666/Bat4WindowsService
    /// </summary>
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        static void Main()
        {
            //初始化配置信息
            ConfigInfo.Init();

            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
                new BatService()
            };
            ServiceBase.Run(ServicesToRun);
        }
    }
}
