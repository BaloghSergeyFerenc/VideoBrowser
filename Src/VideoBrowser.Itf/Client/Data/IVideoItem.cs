namespace VideoBrowser.Itf.Client.Data
{
    public interface IVideoItem
    {
        int VideoId { get; }
        string VideoTitle { get; }
        string VideoDetail { get; }
    }
}
