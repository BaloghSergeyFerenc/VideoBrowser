using Ninject.Modules;
using System.Net.Http;
using VideoBrowser.Client.Data;
using VideoBrowser.Client.Http;
using VideoBrowser.Itf.Client.Data;

namespace VideoBrowser.Client
{
    public class ClientModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IVideoListContentController>().To(typeof(VideoListContentController));
            Bind<IVideoSourceConnector>().To(typeof(VideoSourceConnector));
            Bind<HttpClient>().ToSelf();
        }
    }
}
