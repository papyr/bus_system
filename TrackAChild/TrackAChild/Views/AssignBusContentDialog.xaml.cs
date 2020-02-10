using TrackAChild.ViewModels;
using Windows.UI.Xaml.Controls;
using Microsoft.Extensions.DependencyInjection;

// The Content Dialog item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace TrackAChild.Views
{
    public sealed partial class AssignBusContentDialog : ContentDialog
    {
        public AssignBusViewModel ViewModel { get; } = (App.Current as App).Container.GetService<AssignBusViewModel>();

        public AssignBusContentDialog()
        {
            this.InitializeComponent();

            DataContext = ViewModel;
        }
    }
}
