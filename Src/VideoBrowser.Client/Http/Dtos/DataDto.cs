using System.Collections.Generic;

namespace VideoBrowser.Client.Http.Dtos
{
    class DataDto
    {
        public IEnumerable<GetVideoListDataDto> videos { get; set; }
        public PaginationDto pagination { get; set; }
    }
}
