using System;

using TrackAChild.ViewModels;

using Windows.UI.Xaml.Controls;

namespace TrackAChild.Views
{
    public sealed partial class MainPage : Page
    {
        public MainViewModel ViewModel { get; } = new MainViewModel();

        public MainPage()
        {
            InitializeComponent();
        }
    }
}
