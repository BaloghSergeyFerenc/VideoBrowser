namespace VideoBrowser.Itf
{
    public interface IVideoItem
    {
        int VideoId { get; }
        string VideoTitle { get; }
        string VideoDetail { get; }
    }
}
