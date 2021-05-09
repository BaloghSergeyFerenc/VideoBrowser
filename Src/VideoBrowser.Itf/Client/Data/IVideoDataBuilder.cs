using System.Threading.Tasks;

namespace VideoBrowser.Itf.Client.Data
{
    public interface IVideoDataBuilder
    {
        Task<IVideoData> BuildList(IVideoData current);
    }
}
