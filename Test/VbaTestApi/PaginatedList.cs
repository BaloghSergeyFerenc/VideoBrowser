using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VbaTestApi
{
    public class PaginatedList
    {
        public bool success { get; set; }
        public string status { get; set; }
        public Data data { get; set; }

    }

    public class Data
    {
        public IEnumerable<GetVideoDataDto> videos { get; set; }
        public Pagination pagination { get; set; }
    }

    public class Pagination
    {
        public int total { get; set; }
        public int count { get; set; }
        public int perPage { get; set; }
        public int currentPage { get; set; }
        public int totalPages { get; set; }
    }
}
