using Microsoft.Extensions.DependencyInjection;
using System;
using TrackAChild.Interfaces;
using TrackAChild.Services;
using TrackAChild.ViewModels;
using Windows.ApplicationModel.Activation;
using Windows.UI.Xaml;

namespace TrackAChild
{
    public sealed partial class App : Application
    {
        private Lazy<ActivationService> _activationService;

        private ActivationService ActivationService
        {
            get { return _activationService.Value; }
        }

        public App()
        {
            InitializeComponent();

            // Deferred execution until used. Check https://msdn.microsoft.com/library/dd642331(v=vs.110).aspx for further info on Lazy<T> class.
            _activationService = new Lazy<ActivationService>(CreateActivationService);
        }

        protected override async void OnLaunched(LaunchActivatedEventArgs args)
        {
            Container = RegisterServices();
            if (!args.PrelaunchActivated)
            {
                await ActivationService.ActivateAsync(args);
            }
        }

        protected override async void OnActivated(IActivatedEventArgs args)
        {
            await ActivationService.ActivateAsync(args);
        }

        private ActivationService CreateActivationService()
        {
            return new ActivationService(this, typeof(Views.MainPage), new Lazy<UIElement>(CreateShell));
        }

        private UIElement CreateShell()
        {
            return new Views.ShellPage();
        }

        public IServiceProvider Container { get; private set; }

        private IServiceProvider RegisterServices()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddTransient<DriversViewModel>();
            serviceCollection.AddTransient<DriversNewViewModel>();
            serviceCollection.AddTransient<DriversEditViewModel>();
            serviceCollection.AddTransient<MapViewViewModel>();
            serviceCollection.AddTransient<RoutesViewModel>();
            serviceCollection.AddTransient<RoutesNewViewModel>();
            serviceCollection.AddTransient<SearchAddressViewModel>();
            serviceCollection.AddTransient<NewRouteConfirmationViewModel>();
            serviceCollection.AddTransient<BusesViewModel>();
            serviceCollection.AddTransient<BusesNewViewModel>();
            serviceCollection.AddTransient<BusesEditViewModel>();
            serviceCollection.AddTransient<PassengersViewModel>();
            serviceCollection.AddTransient<PassengersNewViewModel>();
            serviceCollection.AddTransient<PassengerStudentEditViewModel>();
            serviceCollection.AddTransient<PassengerChaperoneEditViewModel>();
            serviceCollection.AddSingleton<IHttpService, HttpService>();
            serviceCollection.AddSingleton<IDriverService, DriverService>();
            serviceCollection.AddSingleton<IRouteService, RouteService>();
            serviceCollection.AddSingleton<IMapService, MapService>();
            serviceCollection.AddSingleton<IStopService, StopService>();
            serviceCollection.AddSingleton<IBusService, BusService>();
            serviceCollection.AddSingleton<IPassengerService, PassengerService>();
            serviceCollection.AddSingleton<IOSPathDependencies, OSPathDependencies>();
            return serviceCollection.BuildServiceProvider();
        }
    }
}
