using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using VideoBrowser.Itf.Client.Data;

namespace VideoBrowser.Client.Http
{
    class VideoSourceConnector
    {
        HttpClient m_ListHttpClient;

        public VideoSourceConnector()
        {
            m_ListHttpClient = new HttpClient();
            m_ListHttpClient.BaseAddress = new Uri(@"https://pt.potwm.com/api/video-promotion/v1/");
        }

        public async Task<IVideoContent> GetInitialVideoDataList(IVideoContent current)
        {
            return null;
        }

        public async Task<IVideoContent> GetNextVideoDataList(IVideoContent current)
        {
            return null;
        }

        public async Task<IVideoContent> GetPreviousVideoDataList(IVideoContent current)
        {
            return null;
        }
    }
}
