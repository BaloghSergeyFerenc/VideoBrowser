using System.Threading.Tasks;

namespace VideoBrowser.Itf
{
    public interface IVideoDataBuilder
    {
        Task<IVideoData> BuildList(IVideoData current);
    }
}
