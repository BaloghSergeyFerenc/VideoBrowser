using System;
using System.Globalization;
using System.IO;
using System.Windows.Data;

namespace VideoBrowser.App
{
    public class VideoImageConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return
                File.Exists($"{Path.GetTempPath()}\\DocklerVideoBrowser\\{value.ToString()}") ?
                $"{Path.GetTempPath()}\\DocklerVideoBrowser\\{value.ToString()}" :
                $"{Environment.CurrentDirectory}\\video_missing.jpeg";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            //TODO
            return null;
        }
    }
}
