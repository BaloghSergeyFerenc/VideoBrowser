using System.Collections.Generic;
using VideoBrowser.Itf.Client.Data;

namespace VideoBrowser.Client.Data
{
    class VideoContent : IVideoContent
    {
        public IList<IVideoItem> VideoItems { get; } = new List<IVideoItem>();

        public IVideoListDetails VideoListDetails { get; } = new VideoListDetails();
    }
}
