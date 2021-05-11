using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VbaTestApi.Controllers;

namespace VbaTestApi
{
    [Route("api/[controller]")]
    [ApiController]
    public class VideosController
    {
        [HttpGet]
        public ActionResult<PaginatedList> GetVideos(/*[FromQuery] string pageIndex*/)
        {
            
            return new PaginatedList()
            {
                status = "kiraly",
                success = true,
                data = new Data()
                {
                    videos = Helper.CreateMockList(),
                    pagination = new Pagination()
                    {
                        total = 2,
                        count = 25,
                        perPage = 25,
                        currentPage = 1,
                        totalPages = 2
                    }
                }
            };
        }
    }
}
