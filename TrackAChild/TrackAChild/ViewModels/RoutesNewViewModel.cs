using System.Windows.Input;
using TrackAChild.Helpers;
using TrackAChild.Views;
using System;
using TrackAChild.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.ObjectModel;
using Windows.UI.Xaml.Controls;

namespace TrackAChild.ViewModels
{
    public class RoutesNewViewModel : Observable
    {
        IStopService stopService;

        private StopModel selectedStop;
        public StopModel SelectedStop
        {
            get { return selectedStop; }
            set { Set(ref selectedStop, value); }
        }

        public ObservableCollection<StopModel> Stops
        {
            get { return stopService.Stops; }
        }

        public ICommand NewStopCommand { get; set; }

        public ICommand RemoveStopCommand { get; set; }

        public ICommand SwitchStopUpCommand { get; set; }

        public ICommand SwitchStopDownCommand { get; set; }

        public ICommand SaveRouteCommand { get; set; }

        public RoutesNewViewModel()
        {
            stopService = (App.Current as App).Container.GetService<IStopService>();

            NewStopCommand = new RelayCommand(async () => { await new SearchAddressDialog().ShowAsync(); });
            RemoveStopCommand = new RelayCommand<object>((param) =>
            {
                // will need to find out what's wrong with this binding at some point
                object stopModel = (param as Grid).DataContext as object;
                stopService.RemoveStop(stopModel);
            });

            SaveRouteCommand = new RelayCommand(async () => { await new NewRouteConfirmation().ShowAsync(); });

            SwitchStopUpCommand = new RelayCommand(() =>
            {
                // Find index of selected item in stops
                var indexOfStop = stopService.RetrieveIndexOfStop(SelectedStop as object);

                // if index is 0, do nothing
                if (indexOfStop == 0)
                {
                    return;
                }

                // Temp model object
                var tempStopToReinsert = SelectedStop.Clone();

                // Remove item from collection and reinsert at desired index (-1)
                stopService.RemoveStop(SelectedStop);

                // SelectedStop becomes null at this point, so let's use the
                // copy we made to reinsert
                stopService.AddStopAtIndex(tempStopToReinsert, indexOfStop - 1);
            });

            SwitchStopDownCommand = new RelayCommand(() =>
            {
                // Find index of selected item in stops
                var indexOfStop = stopService.RetrieveIndexOfStop(SelectedStop as object);

                // if index is the last index, do nothing
                if (indexOfStop == stopService.Stops.Count - 1)
                {
                    return;
                }

                // Temp model object
                var tempStopToReinsert = SelectedStop.Clone();

                // Remove item from collection and reinsert at desired index (+1)
                stopService.RemoveStop(SelectedStop);

                // SelectedStop becomes null at this point, so let's use the
                // copy we made to reinsert
                stopService.AddStopAtIndex(tempStopToReinsert, indexOfStop + 1);
            });
        }
    }
}
