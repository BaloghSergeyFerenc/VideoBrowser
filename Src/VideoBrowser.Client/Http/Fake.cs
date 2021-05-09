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
    class Fake
    {
        HttpClient m_ListHttpClient;
        HttpClient m_ImgHttpClient;

        public Fake()
        {
            m_ListHttpClient = new HttpClient();
            m_ListHttpClient.BaseAddress = new Uri(@"https://localhost:44313/api/");
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
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, "videos");
                HttpResponseMessage response = await m_ListHttpClient.SendAsync(request);
                string content = await response.Content.ReadAsStringAsync();

                PaginatedList videoListDataDtos = JsonConvert.DeserializeObject<PaginatedList>(content);

                foreach (GetVideoListDataDto item in videoListDataDtos.data.videos)
                {
                    HttpResponseMessage c = await m_ImgHttpClient.SendAsync(new HttpRequestMessage(HttpMethod.Get, new Uri($"http:{item.coverImage}")));
                    byte[] cont = await c.Content.ReadAsByteArrayAsync();
                    string imgFileName = item.coverImage.Substring(item.coverImage.LastIndexOf("/") + 1);
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
            catch (Exception e)
            {
                string x = e.Message;
            }
            return current;
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
