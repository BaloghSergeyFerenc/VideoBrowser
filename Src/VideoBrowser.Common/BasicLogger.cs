using System;
using System.IO;
using System.Text;
using VideoBrowser.Itf.Common;

namespace VideoBrowser.Common
{
    public class BasicLogger : ILogger
    {
        private string m_LogPath;
        private StringBuilder m_LogEntryBuilder;
        public BasicLogger()
        {
            m_LogPath = $"{Path.GetTempPath()}\\DocklerVideoBrowser\\";
            if (!Directory.Exists(m_LogPath))
            {
                Directory.CreateDirectory(m_LogPath);
            }
            m_LogEntryBuilder = new StringBuilder();
            m_LogPath = Path.Combine(m_LogPath, "VbaLogs.txt");
            if(File.Exists(m_LogPath))
            {
                File.Delete(m_LogPath);
            }
        }

        public void Log(string logSource, string message, Exception e = null)
        {
            string dateTime = $"{DateTime.Now.ToShortDateString().Replace(" ", "")}-{DateTime.Now.ToShortTimeString().Replace(" ", "")}";
            m_LogEntryBuilder.Append($">>>{logSource} --- {dateTime} --- {message} {Environment.NewLine}");
            if (e != null)
            {
                m_LogEntryBuilder.AppendLine($"EXCEPTION: {e.Message}");
                m_LogEntryBuilder.Append(e.StackTrace);
            }
            m_LogEntryBuilder.AppendLine();
            File.AppendAllText(m_LogPath, m_LogEntryBuilder.ToString());
            m_LogEntryBuilder.Clear();
        }
    }
}
