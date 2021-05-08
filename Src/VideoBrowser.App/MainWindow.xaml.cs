using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace VideoBrowser.App
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public List<VideoItem> VideoList { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            LoadListItems();
        }

        //load list to the ListBox
        private void LoadListItems()
        {
            VideoList = new List<VideoItem>();
            VideoList = CreateVideoList();
            ListBoxConverter.ItemsSource = VideoList;
            this.ListBoxConverter.DataContext = this;
        }

        //creating sample item list
        private List<VideoItem> CreateVideoList()
        {
            for (int i = 1; i < 26; i++)
            {
                VideoList.Add(
                    new VideoItem()
                    {
                        VideoId = i,
                        VideoTitle = "Video Title  " + i,
                        VideoDetail = "Video Detail  " + i
                    });
            }
            return VideoList;
        }
    }

    public class VideoItem
    {
        public int VideoId { get; set; }
        public string VideoTitle { get; set; }
        public string VideoDetail { get; set; }
    }

    public class VideoImageConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return
                File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\images\\" + value.ToString() + ".png") ?
                Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\images\\" + value.ToString() + ".png" :
                Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\images\\video_missing.jpeg";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            //TODO
            return null;
        }
    }
}
