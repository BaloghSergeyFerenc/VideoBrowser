namespace VideoBrowser.Client.Http
{
    class GetVideoListDataDto
    {
        public string id { get; set; }
        public string profileImage { get; set; }
        public string title { get; set; }
        public string uploader { get; set; }
        public string duration { get; set; }
        public string quality { get; set; }
    }
}
