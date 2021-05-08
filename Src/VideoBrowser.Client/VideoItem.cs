using VideoBrowser.Itf;

namespace VideoBrowser.Client
{
    class VideoItem : IVideoItem
    {
        public int VideoId { get; set; }
        public string VideoTitle { get; set; }
        public string VideoDetail { get; set; }
    }
}
