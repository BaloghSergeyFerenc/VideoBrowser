namespace VideoBrowser.Client.Http.Dtos
{
    internal class GetPaginatedListDto
    {
        public bool success { get; set; }
        public string status { get; set; }
        public DataDto data { get; set; }
    }
}
