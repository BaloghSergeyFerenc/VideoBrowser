using Newtonsoft.Json;
using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using VideoBrowser.Client.Data;
using VideoBrowser.Client.Http.Dtos;
using VideoBrowser.Itf.Client.Data;
using VideoBrowser.Itf.Common;

namespace VideoBrowser.Client.Http
{
    internal class VideoSourceConnector : IVideoSourceConnector
    {
        #region Fileds
        private const string f_LogSource = "VideoBrowser.Client.VideoSourceConnector";
        private ulong f_Counter = 0;
        private string f_RequestUriPart = "";
        private string f_ApiUrl = "";

        private ILogger _Logger;
        private IEnvironmentInitializer _EnvironmentInitializer;
        private HttpClient _ListHttpClient;
        private HttpClient _ImgHttpClient;
        #endregion Fileds

        #region Public
        public VideoSourceConnector(ILogger logger, IEnvironmentInitializer environmentInitializer, HttpClient listHttpClient, HttpClient imgHttpClient)
        {
            _Logger = logger;
            _EnvironmentInitializer = environmentInitializer;
            f_ApiUrl = environmentInitializer.GetApiUrl();
            f_RequestUriPart = environmentInitializer.GetRequestUri();
            _ListHttpClient = listHttpClient;
            _ImgHttpClient = imgHttpClient;
            _ListHttpClient.BaseAddress = new Uri(f_ApiUrl);
        }

        public async Task<IVideoContent> GetInitialVideoDataList(IVideoContent current)
        {
            string requestUri = f_RequestUriPart;
            await RequestForVideoContent(current, requestUri);
            return current;
        }

        public async Task<IVideoContent> GetNextVideoDataList(IVideoContent current)
        {
            string requestUri = $"{f_RequestUriPart}&pageIndex={current.VideoListDetails.CurrentPage + 1}";
            await RequestForVideoContent(current, requestUri);
            return current;
        }

        public async Task<IVideoContent> GetPreviousVideoDataList(IVideoContent current)
        {
            string requestUri = $"{f_RequestUriPart}&pageIndex={current.VideoListDetails.CurrentPage - 1}";
            await RequestForVideoContent(current, requestUri);
            return current;
        }
        #endregion Public

        #region Private
        private async Task RequestForVideoContent(IVideoContent current, string RequestUri)
        {
            try
            {
                _Logger.Log(f_LogSource, "Request started.");
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, RequestUri);
                HttpResponseMessage response = await _ListHttpClient.SendAsync(request);
                string content = await response.Content.ReadAsStringAsync();
                GetPaginatedListDto videoListDataDtos = JsonConvert.DeserializeObject<GetPaginatedListDto>(content);

                await VideoDetailsCollector(current, videoListDataDtos);
                current.VideoListDetails.CurrentPage = videoListDataDtos.data.pagination.currentPage;
                current.VideoListDetails.AllPages = videoListDataDtos.data.pagination.totalPages;
                _Logger.Log(f_LogSource, "Request ended.");
            }
            catch (Exception e)
            {
                _Logger.Log(f_LogSource, $"Request failed with exception: {e.Message}", e);
            }
        }

        private async Task VideoDetailsCollector(IVideoContent current, GetPaginatedListDto videoListDataDtos)
        {
            foreach (GetVideoListDataDto item in videoListDataDtos.data.videos)
            {
                HttpResponseMessage c = await _ImgHttpClient.SendAsync(new HttpRequestMessage(HttpMethod.Get, new Uri($"https:{item.profileImage}")));
                byte[] cont = await c.Content.ReadAsByteArrayAsync();
                string imgFileName = MineImgFileName(item.profileImage);
                string imgPath = Path.Combine(Path.GetTempPath(), "DocklerVideoBrowser", imgFileName);
                current.VideoItems.Add(new VideoItem()
                {
                    VideoTitle = item.title,
                    VideoDetail = $"Source: {item.uploader}, Durration: {item.duration} sec, Quality: {item.quality}",
                    VideoId = imgFileName
                });
                File.WriteAllBytes(imgPath, cont);
            }
        }

        private string MineImgFileName(string input)
        {
            //TODO: overflow exception not handled in runtime
            return
                "Local_" + f_Counter++ + input.Substring(input.LastIndexOf("/") + 1)
                    .Substring(0, input.Substring(input.LastIndexOf("/") + 1).LastIndexOf("?"));
        }
        #endregion Private
    }
}
