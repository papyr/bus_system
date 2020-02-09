using TrackAChild.ViewModels;
using Windows.UI.Xaml.Controls;
using Microsoft.Extensions.DependencyInjection;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace TrackAChild.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class DriversPage : Page
    {
        public DriversViewModel ViewModel { get; } = (App.Current as App).Container.GetService<DriversViewModel>();

        public DriversPage()
        {
            this.InitializeComponent();

            DataContext = ViewModel;
        }
    }
}
