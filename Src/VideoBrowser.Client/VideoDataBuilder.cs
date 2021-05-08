using System;
using System.Threading.Tasks;
using VideoBrowser.Itf;

namespace VideoBrowser.Client
{
    public class VideoDataBuilder : IVideoDataBuilder
    {
        public Task<IVideoData> BuildList(IVideoData current)
        {
            return Task.Run(() =>
            {
                return
                    current == null || current.VideoListDetails.PageingState == EPageingState.Reload ?
                    LoadInitialVideoData(current) :
                    UpdateVideoData(current);
            });
        }

        private IVideoData UpdateVideoData(IVideoData current)
        {
            return
                current.VideoListDetails.PageingState == EPageingState.Next ?
                LoadNextPage(current) :
                LoadPreviousPage(current);
        }

        private IVideoData LoadNextPage(IVideoData current)
        {
            if(current.VideoListDetails.CurrentPage < current.VideoListDetails.AllPages)
            {
                current.VideoItems.Clear();
                for (int i = 26; i < 30; i++)
                {
                    current.VideoItems.Add(
                        new VideoItem()
                        {
                            VideoId = i,
                            VideoTitle = "Video Title  " + i,
                            VideoDetail = "Video Detail  " + i
                        });
                }
                current.VideoListDetails.CurrentPage++;
            }
            current.VideoListDetails.PageingState = EPageingState.None;
            return current;
        }

        private IVideoData LoadPreviousPage(IVideoData current)
        {
            if (current.VideoListDetails.CurrentPage > 1)
            {
                current.VideoItems.Clear();
                for (int i = 1; i < 26; i++)
                {
                    current.VideoItems.Add(
                        new VideoItem()
                        {
                            VideoId = i,
                            VideoTitle = "Video Title  " + i,
                            VideoDetail = "Video Detail  " + i
                        });
                }
                current.VideoListDetails.CurrentPage--;
            }
            current.VideoListDetails.PageingState = EPageingState.None;
            return current;
        }

        private IVideoData LoadInitialVideoData(IVideoData current)
        {
            current = new VideoData();
            for (int i = 1; i < 26; i++)
            {
                current.VideoItems.Add(
                    new VideoItem()
                    {
                        VideoId = i,
                        VideoTitle = "Video Title  " + i,
                        VideoDetail = "Video Detail  " + i
                    });
            }
            current.VideoListDetails.CurrentPage = 1;
            current.VideoListDetails.AllPages = 2;
            current.VideoListDetails.PageingState = EPageingState.None;
            return current;
        }
    }
}
