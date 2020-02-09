using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using TrackAChild.Helpers;
using TrackAChild.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Windows.UI.Xaml.Controls;
using TrackAChild.Services;
using TrackAChild.Views;

namespace TrackAChild.ViewModels
{
    public class NewRouteConfirmationViewModel : Observable
    {
        IRouteService routeService;
        IStopService stopService;

        private bool isMondaySelected;
        public bool IsMondaySelected { get { return isMondaySelected; } set { Set(ref isMondaySelected, value); } }

        private bool isTuesdaySelected;
        public bool IsTuesdaySelected { get { return isTuesdaySelected; } set { Set(ref isTuesdaySelected, value); } }

        private bool isWednesdaySelected;
        public bool IsWednesdaySelected { get { return isWednesdaySelected; } set { Set(ref isWednesdaySelected, value); } }

        private bool isThursdaySelected;
        public bool IsThursdaySelected { get { return isThursdaySelected; } set { Set(ref isThursdaySelected, value); } }

        private bool isFridaySelected;
        public bool IsFridaySelected { get { return isFridaySelected; } set { Set(ref isFridaySelected, value); } }

        private bool isSaturdaySelected;
        public bool IsSaturdaySelected { get { return isSaturdaySelected; } set { Set(ref isSaturdaySelected, value); } }

        private bool isSundaySelected;
        public bool IsSundaySelected { get { return isSundaySelected; } set { Set(ref isSundaySelected, value); } }

        private bool isNeverEnding;
        public bool IsNeverEnding { get { return isNeverEnding; } set { Set(ref isNeverEnding, value); } }

        private string routeName;
        public string RouteName { get { return routeName; } set { Set(ref routeName, value); } }

        private DateTimeOffset? minEndDate = DateTime.Now.AddDays(1);
        public DateTimeOffset? MinEndDate { get { return minEndDate; } set { Set(ref minEndDate, value); } }

        // private selected date field
        private DateTimeOffset? selectedDate;

        public ICommand DateChangedCommand { get; private set; }

        public ICommand CreateRouteCommand { get; private set; }

        public NewRouteConfirmationViewModel()
        {
            routeService = (App.Current as App).Container.GetService<IRouteService>();
            stopService = (App.Current as App).Container.GetService<IStopService>();

            CreateRouteCommand = new RelayCommand(() =>
            {
                List<DateTime> datesForRoute = GetDatesSetForRoute();

                if (!string.IsNullOrEmpty(RouteName)
                    && stopService.Stops.Count > 0
                    && datesForRoute.Count > 0)
                {
                    RouteModel routeToAdd = new RouteModel()
                        { RouteName = this.RouteName,
                          Stops = new System.Collections.ObjectModel.ObservableCollection<StopModel>(stopService.Stops),
                          DatesForCurrentRoute = new List<DateTime>(datesForRoute) };

                    // Add route to list of routes
                    routeService.AddRoute(routeToAdd);

                    // Cleanup
                    CleanupRouteCreation();

                    // Navigate to route page
                    NavigationService.Navigate(typeof(RoutesPage));
                }
            });

            DateChangedCommand = new RelayCommand<CalendarDatePicker>((param) =>
            {
                selectedDate = param.Date;
            });
        }

        private void CleanupRouteCreation()
        {
            RouteName = "";
            stopService.Stops.Clear();
        }

        private List<DateTime> GetDatesSetForRoute()
        {
            // Get all dates chosen for the route we are about to save
            List<DateTime> datesToStoreForRoute = new List<DateTime>();

            if (IsMondaySelected)
                datesToStoreForRoute.AddRange(DateTimeHelper.GetEveryXFromYear(DateTime.Now.Year)
                                                  .Where(x => x.DayOfWeek == DayOfWeek.Monday
                                                  && x > DateTime.Now));
            if (IsTuesdaySelected)
                datesToStoreForRoute.AddRange(DateTimeHelper.GetEveryXFromYear(DateTime.Now.Year)
                                                  .Where(x => x.DayOfWeek == DayOfWeek.Tuesday
                                                  && x > DateTime.Now));
            if (IsWednesdaySelected)
                datesToStoreForRoute.AddRange(DateTimeHelper.GetEveryXFromYear(DateTime.Now.Year)
                                                  .Where(x => x.DayOfWeek == DayOfWeek.Wednesday
                                                  && x > DateTime.Now));
            if (IsThursdaySelected)
                datesToStoreForRoute.AddRange(DateTimeHelper.GetEveryXFromYear(DateTime.Now.Year)
                                                  .Where(x => x.DayOfWeek == DayOfWeek.Thursday
                                                  && x > DateTime.Now));
            if (IsFridaySelected)
                datesToStoreForRoute.AddRange(DateTimeHelper.GetEveryXFromYear(DateTime.Now.Year)
                                                  .Where(x => x.DayOfWeek == DayOfWeek.Friday
                                                  && x > DateTime.Now));
            if (IsSaturdaySelected)
                datesToStoreForRoute.AddRange(DateTimeHelper.GetEveryXFromYear(DateTime.Now.Year)
                                                  .Where(x => x.DayOfWeek == DayOfWeek.Saturday
                                                  && x > DateTime.Now));
            if (IsSundaySelected)
                datesToStoreForRoute.AddRange(DateTimeHelper.GetEveryXFromYear(DateTime.Now.Year)
                                                  .Where(x => x.DayOfWeek == DayOfWeek.Sunday
                                                  && x > DateTime.Now));

            // Sort in order from most recent
            datesToStoreForRoute.Sort((x, y) => DateTime.Compare(x, y));

            // Check if there is an end-date we need to filter on
            if (IsNeverEnding)
            {
                datesToStoreForRoute = datesToStoreForRoute.Where(x => x <= selectedDate).ToList();
            }

            return datesToStoreForRoute;
        }
    }
}
