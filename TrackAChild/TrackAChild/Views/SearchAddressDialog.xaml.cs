using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using TrackAChild.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using TrackAChild.Models;

// The Content Dialog item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace TrackAChild.Views
{
    public sealed partial class SearchAddressDialog : ContentDialog
    {
        public SearchAddressViewModel ViewModel { get; } = (App.Current as App).Container.GetService<SearchAddressViewModel>();

        private bool doNotClose = true;

        public SearchAddressDialog()
        {
            this.InitializeComponent();

            DataContext = ViewModel;
        }

        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            if (sender.PrimaryButtonText == "Search")
            {
                doNotClose = true;
            }
            else if (sender.PrimaryButtonText == "Add")
            {
                doNotClose = false;
            }
        }

        private void ContentDialog_Closing(ContentDialog sender, ContentDialogClosingEventArgs args)
        {
            // If result is secondary, we want to close anyway
            if (args.Result == ContentDialogResult.Secondary)
            {
                doNotClose = false;
            }
            else
            {
                // Don't really like this here but...
                if (resultsListView.SelectedItem != null)
                {
                    // only including model to cast
                    StringWrapper wrapper = resultsListView.SelectedItem as StringWrapper;
                    if (!string.IsNullOrEmpty(wrapper.StringContent))
                    {
                        args.Cancel = false;
                    }
                }
            }

            if (doNotClose)
            {
                args.Cancel = true;
            }
        }

        private void ContentDialog_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            doNotClose = false;
        }
    }
}
