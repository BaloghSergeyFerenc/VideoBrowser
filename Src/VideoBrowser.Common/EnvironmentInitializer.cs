using Ninject;
using System;
using System.IO;
using VideoBrowser.Itf.Common;

namespace VideoBrowser.Common
{
    internal class EnvironmentInitializer : IEnvironmentInitializer, IInitializable
    {
        private string f_ApiUrl = "";
        private string f_RequestUri = "";

        public string GetApiUrl()
        {
            return f_ApiUrl;
        }

        public string GetRequestUri()
        {
            return f_RequestUri;
        }

        public void Initialize()
        {
            string workPath = $"{Path.GetTempPath()}\\DocklerVideoBrowser\\";
            string configPath = $"{Environment.CurrentDirectory}\\VideoBrowserConfig.txt";
            if (Directory.Exists(workPath))
            {
                Directory.Delete(workPath, true);
            }
            Directory.CreateDirectory(workPath);

            using (StreamReader sr = new StreamReader(configPath))
            {
                f_ApiUrl = sr.ReadLine();
                f_RequestUri = sr.ReadLine();
            }
        }
    }
}
