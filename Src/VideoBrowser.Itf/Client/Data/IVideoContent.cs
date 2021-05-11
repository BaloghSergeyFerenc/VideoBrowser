using System.Collections.Generic;

namespace VideoBrowser.Itf.Client.Data
{
    public interface IVideoContent
    {
        IList<IVideoItem> VideoItems { get; }
        IVideoListDetails VideoListDetails { get; }
    }
}
