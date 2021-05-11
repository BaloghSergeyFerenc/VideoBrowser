using System.Threading.Tasks;
using VideoBrowser.Client.Http;
using VideoBrowser.Itf.Client.Data;

namespace VideoBrowser.Client.Data
{
    internal class VideoListContentController : IVideoListContentController
    {
        #region Fields
        private IVideoSourceConnector _VideoSource;
        #endregion Fields

        #region Public
        public VideoListContentController(IVideoSourceConnector videoSourceConnector)
        {
            _VideoSource = videoSourceConnector;
        }

        public async Task<IVideoContent> UpdateVideoList(IVideoContent current)
        {
            return
                current == null || current.VideoListDetails.PageingState == EPageingState.Reload || current.VideoListDetails.PageingState == EPageingState.None ?
                await LoadInitialVideoData(current) :
                await UpdateVideoData(current);
        }
        #endregion Public

        #region Private
        private async Task<IVideoContent> UpdateVideoData(IVideoContent current)
        {
            return
                current.VideoListDetails.PageingState == EPageingState.Next ?
                await LoadNextPage(current) :
                await LoadPreviousPage(current);
        }

        private async Task<IVideoContent> LoadNextPage(IVideoContent current)
        {
            if (current.VideoListDetails.CurrentPage < current.VideoListDetails.AllPages)
            {
                current.VideoItems.Clear();
                await _VideoSource.GetNextVideoDataList(current);
            }
            current.VideoListDetails.PageingState = EPageingState.None;
            return current;
        }

        private async Task<IVideoContent> LoadPreviousPage(IVideoContent current)
        {
            if (current.VideoListDetails.CurrentPage > 1)
            {
                current.VideoItems.Clear();
                await _VideoSource.GetPreviousVideoDataList(current);
            }
            current.VideoListDetails.PageingState = EPageingState.None;
            return current;
        }

        private async Task<IVideoContent> LoadInitialVideoData(IVideoContent current)
        {
            current = new VideoContent();
            await _VideoSource.GetInitialVideoDataList(current);
            current.VideoListDetails.PageingState = EPageingState.None;
            return current;
        }
        #endregion Private
    }
}
