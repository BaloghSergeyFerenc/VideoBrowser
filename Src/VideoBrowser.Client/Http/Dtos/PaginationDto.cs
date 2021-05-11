namespace VideoBrowser.Client.Http.Dtos
{
    class PaginationDto
    {
        public int total { get; set; }
        public int count { get; set; }
        public int perPage { get; set; }
        public int currentPage { get; set; }
        public int totalPages { get; set; }
    }
}
