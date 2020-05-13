using System;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace Bat4WindowsService
{
    /// <summary>
    /// @author: liusx
    /// @repository: https://github.com/l634666/Bat4WindowsService
    /// </summary>
    internal class ConfigInfo
    {
        /// <summary>
        /// 日志配置项key
        /// </summary>
        private static readonly string LOG_FILE_PATH_KEY = "LogFilePath";
        /// <summary>
        /// 环境脚本配置项key
        /// </summary>
        private static readonly string SETENV_BAT_FILE_PATH_KEY = "SetenvBatFilePath";
        /// <summary>
        /// 启动脚本配置项key
        /// </summary>
        private static readonly string STARTUP_BAT_FILE_PATH_KEY = "StartupBatFilePath";
        /// <summary>
        /// 停止脚本配置项key
        /// </summary>
        private static readonly string SHUTDOWN_BAT_FILE_PATH_KEY = "ShutdownBatFilePath";

        /// <summary>
        /// 日志配置项key
        /// </summary>
        private static string  _LogFilePath = "";
        /// <summary>
        /// 环境脚本配置项
        /// </summary>
        private static string _SetenvBatFilePath;
        /// <summary>
        /// 启动脚本配置项
        /// </summary>
        private static string _StartupBatFilePath;
        /// <summary>
        /// 停止脚本配置项
        /// </summary>
        private static string _ShutdownBatFilePath;

        /// <summary>
        /// 私有构造，不允许创建实例
        /// </summary>
        private ConfigInfo() { }

        /// <summary>
        /// 设置信息
        /// </summary>
        private static Dictionary<string, string> _AppSettings = new Dictionary<string, string>();


        /// <summary>
        /// 初始化配置信息
        /// </summary>
        internal static void Init()
        {
            //从配置文件加载配置项
            LoadAppSettings();
        }

        /// <summary>
        /// 加载配置项
        /// </summary>
        private static void LoadAppSettings()
        {
            NameValueCollection settings = System.Configuration.ConfigurationManager.AppSettings;

            string[] keys = settings.AllKeys;

            foreach (string key in keys)
            {
                _AppSettings.Add(key, settings[key]);
            }

            // 日志文件配置项
            if (_AppSettings.ContainsKey(LOG_FILE_PATH_KEY))
            {
                _LogFilePath = _AppSettings[LOG_FILE_PATH_KEY];
            }

            // 环境脚本配置项
            if (_AppSettings.ContainsKey(SETENV_BAT_FILE_PATH_KEY))
            {
                _SetenvBatFilePath = _AppSettings[SETENV_BAT_FILE_PATH_KEY];
            }

            //启动脚本配置项
            if (_AppSettings.ContainsKey(STARTUP_BAT_FILE_PATH_KEY))
            {
                _StartupBatFilePath = _AppSettings[STARTUP_BAT_FILE_PATH_KEY];
            }

            //停止脚本配置项
            if (_AppSettings.ContainsKey(SHUTDOWN_BAT_FILE_PATH_KEY))
            {
                _ShutdownBatFilePath = _AppSettings[SHUTDOWN_BAT_FILE_PATH_KEY];
            }

        }


        /// <summary>
        /// 获取设置项的值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        private static string getAppSettingValue(string key)
        {
            return getAppSettingValue(key, "");
        }

        /// <summary>
        /// 获取设置项的值，传入默认值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        private static string getAppSettingValue(string key, string defaultValue)
        {
            string value = "";

            if (_AppSettings.ContainsKey(key))
            {
                value = _AppSettings[key];
            }

            return String.IsNullOrEmpty(value) ? defaultValue : value;
        }

        /// <summary>
        /// 日志文件配置项
        /// </summary>
        /// 
        public static string LogFilePath { get => _LogFilePath; }

        /// <summary>
        /// 环境脚本配置项
        /// </summary>
        public static string SetenvBatFilePath
        {
            get => _SetenvBatFilePath;
        }
        /// <summary>
        /// 启动脚本配置项
        /// </summary>
        public static string StartupBatFilePath { get => _StartupBatFilePath; }
        /// <summary>
        /// 停止脚本配置项
        /// </summary>
        /// 
        public static string ShutdownBatFilePath { get => _ShutdownBatFilePath; }
    }
}
