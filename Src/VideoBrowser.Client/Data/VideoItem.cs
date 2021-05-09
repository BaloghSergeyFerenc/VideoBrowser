using VideoBrowser.Itf.Client.Data;

namespace VideoBrowser.Client.Data
{
    class VideoItem : IVideoItem
    {
        public int VideoId { get; set; }
        public string VideoTitle { get; set; }
        public string VideoDetail { get; set; }
    }
}
