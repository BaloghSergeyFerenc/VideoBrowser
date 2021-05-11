using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using VideoBrowser.Client.Data;
using VideoBrowser.Client.Http.Dtos;
using VideoBrowser.Itf.Client.Data;

namespace VideoBrowser.Client.Http
{
    class VideoSourceConnector
    {
        private const string reqUriPart = "list?psid=Guest13&pstool=421_1&accessKey=300657b0409c3a36ad8c16387e75f417&ms_notrack=1&program=revs&campaign_id=&type=&site=jasmin&sexualOrientation=straight&forcedPerformers=&limit=25&primaryColor=%238AC437&labelColor=%23212121&clientIp=172.26.32.1";
        private const string apiUrl = @"https://pt.potwm.com/api/video-promotion/v1/";
        HttpClient m_ListHttpClient;
        HttpClient m_ImgHttpClient;

        public VideoSourceConnector()
        {
            m_ListHttpClient = new HttpClient();
            m_ImgHttpClient = new HttpClient();
            m_ListHttpClient.BaseAddress = new Uri(apiUrl);
        }

        public async Task<IVideoContent> GetInitialVideoDataList(IVideoContent current)
        {
            string requestUri = reqUriPart;
            await RequestForVideoContent(current, requestUri);
            return current;
        }

        public async Task<IVideoContent> GetNextVideoDataList(IVideoContent current)
        {
            string requestUri = $"{reqUriPart}&pageIndex={current.VideoListDetails.CurrentPage++}";
            await RequestForVideoContent(current, requestUri);
            return current;
        }

        public async Task<IVideoContent> GetPreviousVideoDataList(IVideoContent current)
        {
            string requestUri = $"{reqUriPart}&pageIndex={current.VideoListDetails.CurrentPage--}";
            await RequestForVideoContent(current, requestUri);
            return current;
        }

        private async Task RequestForVideoContent(IVideoContent current, string RequestUri)
        {
            try
            {
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, RequestUri);
                HttpResponseMessage response = await m_ListHttpClient.SendAsync(request);
                string content = await response.Content.ReadAsStringAsync();
                GetPaginatedListDto videoListDataDtos = JsonConvert.DeserializeObject<GetPaginatedListDto>(content);

                await VideoDetailsCollector(current, videoListDataDtos);
                current.VideoListDetails.CurrentPage = videoListDataDtos.data.pagination.currentPage;
                current.VideoListDetails.AllPages = videoListDataDtos.data.pagination.totalPages;
            }
            catch (Exception e)
            {
                //LOG
            }
        }

        private async Task VideoDetailsCollector(IVideoContent current, GetPaginatedListDto videoListDataDtos)
        {
            foreach (GetVideoListDataDto item in videoListDataDtos.data.videos)
            {
                HttpResponseMessage c = await m_ImgHttpClient.SendAsync(new HttpRequestMessage(HttpMethod.Get, new Uri($"https:{item.profileImage}")));
                byte[] cont = await c.Content.ReadAsByteArrayAsync();
                string imgFileName = MineImgFileName(item.profileImage);
                string imgPath = Path.Combine(Path.GetTempPath(), "DocklerVideoBrowser", imgFileName);
                current.VideoItems.Add(new VideoItem()
                {
                    VideoTitle = item.title,
                    VideoDetail = $"{item.uploader} {item.duration} {item.quality}",
                    VideoId = imgFileName
                });
                File.WriteAllBytes(imgPath, cont);
            }
        }

        private string MineImgFileName(string input)
        {
            //TODO: bug is here, no unique local filenames
            return
                input.Substring(input.LastIndexOf("/") + 1)
                    .Substring(0, input.Substring(input.LastIndexOf("/") + 1).LastIndexOf("?"));
        }
    }
}
