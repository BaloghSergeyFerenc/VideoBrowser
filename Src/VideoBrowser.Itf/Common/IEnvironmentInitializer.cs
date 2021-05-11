namespace VideoBrowser.Itf.Common
{
    public interface IEnvironmentInitializer
    {
        string GetRequestUri();
        string GetApiUrl();
    }
}
