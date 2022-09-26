using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using UnityEngine;
using System.IO;
using System.Security;

public interface ILogger
{
    void Log(string condition, string stackTrace, UnityEngine.LogType type);
}

public class FileLogger : ILogger
{
    private readonly string path;
 
    /// <summary>
    /// 构造方法
    /// </summary>
    /// <param name="isClear">是否清空原有的日志</param>
    public FileLogger(bool isClear = false)
    {
        switch (Application.platform)
        {
            case RuntimePlatform.Android:
                path = Path.Combine(Application.persistentDataPath,"log.txt");
                break;
            case RuntimePlatform.WindowsPlayer:
                CreatePath(Path.Combine(Application.dataPath,"../Logs"));
                path = Path.Combine(Application.dataPath, GetLogFileName());
                break;
            case RuntimePlatform.WindowsEditor:
                CreatePath(Path.Combine(Application.dataPath,"../Logs"));
                path = Path.Combine(Application.dataPath, GetLogFileName());
                break;
            case RuntimePlatform.IPhonePlayer:
                path = Path.Combine(Application.persistentDataPath, "log.txt");
                break;
            case RuntimePlatform.OSXEditor:
                break;
            default:
                break;
        }
 
        if (isClear)
        {
            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }
    }
 
    public void Log(string condition, string stackTrace, LogType type)
    {
        using (StreamWriter sw = new StreamWriter(path, true, System.Text.Encoding.UTF8))
        {
            string msg = string.Format("[{0}] {1}: {2}\n{3}", GetNowTime(), type, condition, stackTrace);
            sw.WriteLine(msg);
        }
    }
 
    #region Tool Method
    private string GetNowTime()
    {
        return DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
    }

    private string GetLogFileName()
    {
        return "../Logs/"+DateTime.Now.ToString("yyyyMMddHHmmss")+".txt";
    }

    private void CreatePath(string sPath)
    {
        if (!Directory.Exists(sPath))
        {
            Directory.CreateDirectory(sPath);
        }
    }
    #endregion
}

public class LogSys
{
    private static ILogger logger;
    public static ILogger Logger
    {
        get { return logger; }
    }
 
    public bool IsOpen
    {
        get { return Debug.unityLogger.logEnabled; }
    }
 
    private LogSys() { }
 
    /// <summary>
    /// 初始化
    /// </summary>
    /// <param name="_logger">日志输出器</param>
    /// <param name="isOpen">是否开启日志输出</param>
    public static void Init(ILogger _logger, bool isOpen = true)
    {
        Init(isOpen);
        logger = _logger;
        Enable();
    }
 
    public static void Init(bool isOpen = true)
    {
        Debug.unityLogger.logEnabled = isOpen;
    }
 
    /// <summary>
    /// 过滤器
    /// </summary>
    /// <param name="logType">需要显示的日志类型</param>
    public static void Filter(LogType logType = LogType.Log)
    {
        Debug.unityLogger.filterLogType = logType;
    }
 
    public static void Enable()
    {
        if (logger != null)
        {
            Application.logMessageReceived += logger.Log;
        }
    }
 
    public static void Disable()
    {
        if (logger != null)
        {
            Application.logMessageReceived -= logger.Log;
        }
 
    }
}