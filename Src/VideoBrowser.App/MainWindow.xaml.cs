using Ninject;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using VideoBrowser.Client;
using VideoBrowser.Client.Data;
using VideoBrowser.Common;
using VideoBrowser.Itf.Client.Data;
using VideoBrowser.Itf.Common;

namespace VideoBrowser.App
{
    public partial class MainWindow : Window
    {
        #region Fields
        private const string m_LogSource = "VideoBrowser.App.MainWindow";
        private StandardKernel m_Kernel;
        private IVideoListContentController m_VideoListContentController;
        private ILogger m_Logger;

        private IVideoContent CurrentVideoData { get; set; }
        private ObservableCollection<IVideoItem> VideoList { get; set; }
        #endregion Fields

        #region Public
        public MainWindow()
        {
            InitializeComponent();
            this.ListBoxConverter.DataContext = this;
            m_Kernel = GetKernel();
            m_VideoListContentController = m_Kernel.Get<IVideoListContentController>();
            m_Logger = m_Kernel.Get<ILogger>();
        }

        #endregion Public

        #region Buttons
        private async void LoadButton(object sender, RoutedEventArgs args)
        {
            m_Logger.Log(m_LogSource, "Load Button pressed.");
            if(CurrentVideoData != null)
            {
                CurrentVideoData.VideoListDetails.PageingState = EPageingState.Reload;
            }
            CurrentVideoData =  await m_VideoListContentController.UpdateVideoList(CurrentVideoData);
            VideoList = new ObservableCollection<IVideoItem>(CurrentVideoData.VideoItems);
            PagerInfoText.Text = CurrentVideoData.VideoListDetails.PagerInfoText;
            ListBoxConverter.ItemsSource = VideoList;
        }

        private async void NextButton(object sender, RoutedEventArgs args)
        {
            m_Logger.Log(m_LogSource, "Next Button pressed.");
            if(CurrentVideoData != null)
            {
                CurrentVideoData.VideoListDetails.PageingState = EPageingState.Next;
                CurrentVideoData = await m_VideoListContentController.UpdateVideoList(CurrentVideoData);
                VideoList = new ObservableCollection<IVideoItem>(CurrentVideoData.VideoItems);
                PagerInfoText.Text = CurrentVideoData.VideoListDetails.PagerInfoText;
                ListBoxConverter.ItemsSource = VideoList;
            }
        }

        private async void PreviousButton(object sender, RoutedEventArgs args)
        {
            m_Logger.Log(m_LogSource, "Previous Button pressed.");
            if (CurrentVideoData != null)
            {
                CurrentVideoData.VideoListDetails.PageingState = EPageingState.Previous;
                CurrentVideoData = await m_VideoListContentController.UpdateVideoList(CurrentVideoData);
                VideoList = new ObservableCollection<IVideoItem>(CurrentVideoData.VideoItems);
                PagerInfoText.Text = CurrentVideoData.VideoListDetails.PagerInfoText;
                ListBoxConverter.ItemsSource = VideoList;
            }
        }
        #endregion Buttons

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            m_Kernel.Dispose();
        }

        private StandardKernel GetKernel()
        {
            return new StandardKernel(new ClientModule(), new CommonModule());
        }
    }
}
