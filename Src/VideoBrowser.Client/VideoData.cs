using System.Collections.Generic;
using VideoBrowser.Itf;

namespace VideoBrowser.Client
{
    class VideoData : IVideoData
    {
        public IList<IVideoItem> VideoItems { get; } = new List<IVideoItem>();

        public IVideoListDetails VideoListDetails { get; } = new VideoListDetails();
    }
}
