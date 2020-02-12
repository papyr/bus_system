using TrackAChild.ViewModels;
using Windows.UI.Xaml.Controls;
using Microsoft.Extensions.DependencyInjection;

// The Content Dialog item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace TrackAChild.Views
{
    public sealed partial class AssignPassengersContentDialog : ContentDialog
    {
        public AssignPassengersViewModel ViewModel { get; } = (App.Current as App).Container.GetService<AssignPassengersViewModel>();

        public AssignPassengersContentDialog()
        {
            this.InitializeComponent();

            DataContext = ViewModel;
        }
    }
}
