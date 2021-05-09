using System.Collections.Generic;

namespace VideoBrowser.Itf.Client.Data
{
    public interface IVideoData
    {
        IList<IVideoItem> VideoItems { get; }
        IVideoListDetails VideoListDetails { get; }
    }
}
