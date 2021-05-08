namespace VideoBrowser.Itf
{
    public enum EPageingState
    {
        None,
        Reload,
        Next,
        Previous
    }

    public interface IVideoListDetails
    {
        int CurrentPage { get; set; }
        int AllPages { get; set; }
        EPageingState PageingState { get; set; }
        string PagerInfoText { get; }
    }
}