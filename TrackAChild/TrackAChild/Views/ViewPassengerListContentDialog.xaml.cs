using TrackAChild.ViewModels;
using Windows.UI.Xaml.Controls;
using Microsoft.Extensions.DependencyInjection;

// The Content Dialog item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace TrackAChild.Views
{
    public sealed partial class ViewPassengerListContentDialog : ContentDialog
    {
        public ViewPassengerListViewModel ViewModel { get; } = (App.Current as App).Container.GetService<ViewPassengerListViewModel>();

        public ViewPassengerListContentDialog()
        {
            this.InitializeComponent();

            DataContext = ViewModel;
        }
    }
}
