using Windows.UI.Xaml.Controls;
using Microsoft.Extensions.DependencyInjection;
using TrackAChild.ViewModels;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace TrackAChild.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MapViewPage : Page
    {
        public MapViewViewModel ViewModel { get; } = (App.Current as App).Container.GetService<MapViewViewModel>();

        public MapViewPage()
        {
            this.InitializeComponent();

            DataContext = ViewModel;
        }
    }
}
