using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VbaTestApi.Controllers
{
    public class Helper
    {
        public static IEnumerable<GetVideoDataDto> CreateMockList()
        {
            List<GetVideoDataDto> videos = new List<GetVideoDataDto>();

            for (int i = 0; i < 25; i++)
            {
                videos.Add(new GetVideoDataDto()
                {
                    id = "id" + i,
                    title = "title" + i,
                    duration = "0:00",
                    tags = new List<string>() { "tag1", "tag2" },
                    profileImage = @"//galleryn0.awemwh.com/f8d2e11bd6c43618af00d6f28c91232a10/68837c4dd01dfb71a8cd868887976b25.jpg",
                    coverImage = @"//galleryn0.awemwh.com/f8d2e11bd6c43618af00d6f28c91232a10/68837c4dd01dfb71a8cd868887976b25.jpg",
                    previewImages = new List<string>() { @"//galleryn0.awemwh.com/f8d2e11bd6c43618af00d6f28c91232a10/68837c4dd01dfb71a8cd868887976b25.jpg", @"//galleryn0.awemwh.com/f8d2e11bd6c43618af00d6f28c91232a10/68837c4dd01dfb71a8cd868887976b25.jpg" },
                    targetUrl = "url_abc",
                    detailsUrl = "url_abc",
                    quality = "low",
                    isHd = false,
                    uploader = "ribi",
                    uploaderLink = "www.ribi.com"
                });
            }
            return videos;
        }
    }
}
