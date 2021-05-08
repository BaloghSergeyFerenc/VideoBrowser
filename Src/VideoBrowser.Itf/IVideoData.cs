using System.Collections.Generic;

namespace VideoBrowser.Itf
{
    public interface IVideoData
    {
        IList<IVideoItem> VideoItems { get; }
        IVideoListDetails VideoListDetails { get; }
    }
}
