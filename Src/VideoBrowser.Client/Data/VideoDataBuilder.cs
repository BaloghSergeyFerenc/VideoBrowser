using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using VideoBrowser.Client.Http;
using VideoBrowser.Itf.Client.Data;

namespace VideoBrowser.Client.Data
{
    public class VideoDataBuilder : IVideoDataBuilder
    {
        private HttpService m_HttpService;
        public VideoDataBuilder()
        {
            m_HttpService = new HttpService();
            //Task.Run(() => new HttpService().GetVideoDataList(new VideoData()));
        }

        public async Task<IVideoData> BuildList(IVideoData current)
        {
            return
                current == null || current.VideoListDetails.PageingState == EPageingState.Reload ?
                await LoadInitialVideoData(current) :
                await UpdateVideoData(current);
        }

        private async Task<IVideoData> UpdateVideoData(IVideoData current)
        {
            return
                current.VideoListDetails.PageingState == EPageingState.Next ?
                await LoadNextPage(current) :
                await LoadPreviousPage(current);
        }

        private async Task<IVideoData> LoadNextPage(IVideoData current)
        {
            if(current.VideoListDetails.CurrentPage < current.VideoListDetails.AllPages)
            {
                current.VideoItems.Clear();
                //for (int i = 26; i < 30; i++)
                //{
                //    current.VideoItems.Add(
                //        new VideoItem()
                //        {
                //            VideoId = i + ".png",
                //            VideoTitle = "Video Title  " + i,
                //            VideoDetail = "Video Detail  " + i
                //        });
                //}
                await m_HttpService.GetVideoDataList(current);
                current.VideoListDetails.CurrentPage++;
            }
            current.VideoListDetails.PageingState = EPageingState.None;
            return current;
        }

        private async Task<IVideoData> LoadPreviousPage(IVideoData current)
        {
            if (current.VideoListDetails.CurrentPage > 1)
            {
                current.VideoItems.Clear();
                //for (int i = 1; i < 26; i++)
                //{
                //    current.VideoItems.Add(
                //        new VideoItem()
                //        {
                //            VideoId = i + ".png",
                //            VideoTitle = "Video Title  " + i,
                //            VideoDetail = "Video Detail  " + i
                //        });
                //}
                await m_HttpService.GetVideoDataList(current);

                current.VideoListDetails.CurrentPage--;
            }
            current.VideoListDetails.PageingState = EPageingState.None;
            return current;
        }

        private async Task<IVideoData> LoadInitialVideoData(IVideoData current)
        {
            current = new VideoData();
            //for (int i = 1; i < 26; i++)
            //{
            //    current.VideoItems.Add(
            //        new VideoItem()
            //        {
            //            VideoId = i + ".png",
            //            VideoTitle = "Video Title  " + i,
            //            VideoDetail = "Video Detail  " + i
            //        });
            //}
            await m_HttpService.GetVideoDataList(current);

            current.VideoListDetails.CurrentPage = 1;
            current.VideoListDetails.AllPages = 2;
            current.VideoListDetails.PageingState = EPageingState.None;
            return current;
        }
    }
}
