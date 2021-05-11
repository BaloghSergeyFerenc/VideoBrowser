using System;
using System.IO;
using System.Text;
using VideoBrowser.Itf.Common;

namespace VideoBrowser.Common
{
    internal class BasicLogger : ILogger
    {
        #region Fields
        private string m_LogPath;
        private StringBuilder _LogEntryBuilder;
        #endregion Fields

        #region Public
        public BasicLogger()
        {
            m_LogPath = $"{Path.GetTempPath()}\\DocklerVideoBrowser\\";
            if (!Directory.Exists(m_LogPath))
            {
                Directory.CreateDirectory(m_LogPath);
            }
            _LogEntryBuilder = new StringBuilder();
            m_LogPath = Path.Combine(m_LogPath, "VbaLogs.txt");
            if (File.Exists(m_LogPath))
            {
                File.Delete(m_LogPath);
            }
        }

        public void Log(string logSource, string message, Exception e = null)
        {
            string dateTime = $"{DateTime.Now.ToShortDateString().Replace(" ", "")}-{DateTime.Now.ToShortTimeString().Replace(" ", "")}";
            _LogEntryBuilder.Append($">>>{logSource} --- {dateTime} --- {message} {Environment.NewLine}");
            if (e != null)
            {
                _LogEntryBuilder.AppendLine($"EXCEPTION: {e.Message}");
                _LogEntryBuilder.Append(e.StackTrace);
            }
            _LogEntryBuilder.AppendLine();
            File.AppendAllText(m_LogPath, _LogEntryBuilder.ToString());
            _LogEntryBuilder.Clear();
        }
        #endregion Public
    }
}
