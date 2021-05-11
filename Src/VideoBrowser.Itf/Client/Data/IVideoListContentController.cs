using System.Threading.Tasks;

namespace VideoBrowser.Itf.Client.Data
{
    public interface IVideoListContentController
    {
        Task<IVideoContent> UpdateVideoList(IVideoContent current);
    }
}
