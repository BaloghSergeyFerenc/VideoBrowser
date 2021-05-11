using VideoBrowser.Itf.Client.Data;

namespace VideoBrowser.Client.Data
{
    internal class VideoListDetails : IVideoListDetails
    {
        public int CurrentPage { get; set; } = 1;
        public int AllPages { get; set; } = 1;
        public EPageingState PageingState { get; set; }
        public string PagerInfoText
        {
            get
            {
                return $"{CurrentPage} / {AllPages}";
            }
        }
    }
}
