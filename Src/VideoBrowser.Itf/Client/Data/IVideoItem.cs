namespace VideoBrowser.Itf.Client.Data
{
    public interface IVideoItem
    {
        string VideoId { get; }
        string VideoTitle { get; }
        string VideoDetail { get; }
    }
}
