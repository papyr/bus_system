using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using TrackAChild.ViewModels;
using Windows.UI.Xaml.Controls;
using Microsoft.Extensions.DependencyInjection;

// The Content Dialog item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace TrackAChild.Views
{
    public sealed partial class AssignDriverContentDialog : ContentDialog
    {
        public AssignDriverViewModel ViewModel { get; } = (App.Current as App).Container.GetService<AssignDriverViewModel>();

        public AssignDriverContentDialog()
        {
            this.InitializeComponent();

            DataContext = ViewModel;
        }
    }
}
