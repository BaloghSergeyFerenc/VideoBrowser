using System.Threading.Tasks;
using VideoBrowser.Client.Http;
using VideoBrowser.Itf.Client.Data;

namespace VideoBrowser.Client.Data
{
    public class VideoListContentController : IVideoListContentController
    {
        private VideoSourceConnector m_VideoSource;
        public VideoListContentController()
        {
            m_VideoSource = new VideoSourceConnector();
        }

        public async Task<IVideoContent> UpdateVideoList(IVideoContent current)
        {
            return
                current == null || current.VideoListDetails.PageingState == EPageingState.Reload || current.VideoListDetails.PageingState == EPageingState.None ?
                await LoadInitialVideoData(current) :
                await UpdateVideoData(current);
        }

        private async Task<IVideoContent> UpdateVideoData(IVideoContent current)
        {
            return
                current.VideoListDetails.PageingState == EPageingState.Next ?
                await LoadNextPage(current) :
                await LoadPreviousPage(current);
        }

        private async Task<IVideoContent> LoadNextPage(IVideoContent current)
        {
            if(current.VideoListDetails.CurrentPage < current.VideoListDetails.AllPages)
            {
                current.VideoItems.Clear();
                await m_VideoSource.GetNextVideoDataList(current);
            }
            current.VideoListDetails.PageingState = EPageingState.None;
            return current;
        }

        private async Task<IVideoContent> LoadPreviousPage(IVideoContent current)
        {
            if (current.VideoListDetails.CurrentPage > 1)
            {
                current.VideoItems.Clear();
                await m_VideoSource.GetPreviousVideoDataList(current);
            }
            current.VideoListDetails.PageingState = EPageingState.None;
            return current;
        }

        private async Task<IVideoContent> LoadInitialVideoData(IVideoContent current)
        {
            current = new VideoContent();
            await m_VideoSource.GetInitialVideoDataList(current);
            current.VideoListDetails.PageingState = EPageingState.None;
            return current;
        }
    }
}
