using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using VideoBrowser.Client.Data;
using VideoBrowser.Itf.Client.Data;

namespace VideoBrowser.Client.Http
{
    class Fake2
    {
        HttpClient m_ListHttpClient;
        HttpClient m_ImgHttpClient;

        public Fake2()
        {
            m_ListHttpClient = new HttpClient();
            m_ListHttpClient.BaseAddress = new Uri(@"https://pt.potwm.com/api/video-promotion/v1/");
            m_ImgHttpClient = new HttpClient();
            if (!Directory.Exists(Path.Combine(Path.GetTempPath(), "DocklerVideoBrowser")))
            {
                Directory.CreateDirectory(Path.Combine(Path.GetTempPath(), "DocklerVideoBrowser"));
            }
        }

        public async Task<IVideoData> GetVideoDataList(IVideoData current)
        {
            try
            {
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, "list?psid=Guest13&pstool=421_1&accessKey=300657b0409c3a36ad8c16387e75f417&ms_notrack=1&program=revs&campaign_id=&type=&site=jasmin&sexualOrientation=straight&forcedPerformers=&limit=25&primaryColor=%238AC437&labelColor=%23212121&clientIp=172.26.32.1");
                HttpResponseMessage response = await m_ListHttpClient.SendAsync(request);
                string content = await response.Content.ReadAsStringAsync();

                PaginatedList videoListDataDtos = JsonConvert.DeserializeObject<PaginatedList>(content);

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
                current.VideoListDetails.CurrentPage = videoListDataDtos.data.pagination.currentPage;
                current.VideoListDetails.AllPages = videoListDataDtos.data.pagination.totalPages;
                //current.VideoListDetails.PageingState = EPageingState.None;
            }
            catch (Exception e)
            {
                string x = e.Message;
            }
            return current;
        }

        private string MineImgFileName(string input)
        {
            return
                input.Substring(input.LastIndexOf("/") + 1)
                    .Substring(0, input.Substring(input.LastIndexOf("/") + 1).LastIndexOf("?"));
        }
    }

    class PaginatedList
    {
        public bool success { get; set; }
        public string status { get; set; }
        public Data data { get; set; }

    }

    class Data
    {
        public IEnumerable<GetVideoListDataDto> videos { get; set; }
        public Pagination pagination { get; set; }
    }

    class Pagination
    {
        public int total { get; set; }
        public int count { get; set; }
        public int perPage { get; set; }
        public int currentPage { get; set; }
        public int totalPages { get; set; }
    }
}
