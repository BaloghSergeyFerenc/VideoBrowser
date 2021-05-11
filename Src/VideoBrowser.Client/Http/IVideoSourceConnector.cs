using System.Threading.Tasks;
using VideoBrowser.Itf.Client.Data;

namespace VideoBrowser.Client.Http
{
    internal interface IVideoSourceConnector
    {
        Task<IVideoContent> GetInitialVideoDataList(IVideoContent current);
        Task<IVideoContent> GetNextVideoDataList(IVideoContent current);
        Task<IVideoContent> GetPreviousVideoDataList(IVideoContent current);
    }
}