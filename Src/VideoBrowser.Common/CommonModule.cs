using Ninject.Modules;
using VideoBrowser.Itf.Common;

namespace VideoBrowser.Common
{
    public class CommonModule : NinjectModule
    {
        public override void Load()
        {
            Bind<ILogger>().To(typeof(BasicLogger)).InSingletonScope();
            Bind<IEnvironmentInitializer>().To(typeof(EnvironmentInitializer)).InSingletonScope();
        }
    }
}
