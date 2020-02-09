using Microsoft.Extensions.DependencyInjection;
using TrackAChild.ViewModels;
using Windows.UI.Xaml.Controls;

// The Content Dialog item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace TrackAChild.Views
{
    public sealed partial class NewRouteConfirmation : ContentDialog
    {
        public NewRouteConfirmationViewModel ViewModel { get; } = (App.Current as App).
            Container.GetService<NewRouteConfirmationViewModel>();
        public NewRouteConfirmation()
        {
            this.InitializeComponent();

            DataContext = ViewModel;
        }

        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
        }

        private void ContentDialog_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
        }
    }
}
