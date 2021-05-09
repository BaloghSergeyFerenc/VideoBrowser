using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VbaTestApi
{
    public class GetVideoDataDto
    {
        public string id { get; set; }
        public string title { get; set; }
        public string duration { get; set; }
        public List<string> tags { get; set; }
        public string profileImage { get; set; }
        public string coverImage { get; set; }
        public List<string> previewImages { get; set; }
        public string targetUrl { get; set; }
        public string detailsUrl { get; set; }
        public string quality { get; set; }
        public bool isHd { get; set; }
        public string uploader { get; set; }
        public string uploaderLink { get; set; }
    }
}
