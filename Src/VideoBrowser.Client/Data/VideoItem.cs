using VideoBrowser.Itf.Client.Data;

namespace VideoBrowser.Client.Data
{
    internal class VideoItem : IVideoItem
    {
        public string VideoId { get; set; }
        public string VideoTitle { get; set; }
        public string VideoDetail { get; set; }
    }
}
