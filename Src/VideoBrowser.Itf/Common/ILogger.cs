using System;

namespace VideoBrowser.Itf.Common
{
    public interface ILogger
    {
        void Log(string logSource, string message, Exception e = null);
    }
}
