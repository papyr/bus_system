using TrackAChild.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using TrackAChild.Helpers;
using System.Windows.Input;
using System.Collections.ObjectModel;
using Newtonsoft.Json;
using System.Collections.Generic;
using TrackAChild.Models;

namespace TrackAChild.ViewModels
{
    public class SearchAddressViewModel : Observable
    {
        IMapService mapService;
        IHttpService httpService;
        IStopService stopService;

        private StopModel tempModelToStore;

        private string searchAddButtonText = "Search";
        public string SearchAddButtonText
        {
            get { return searchAddButtonText; }
            set { Set(ref searchAddButtonText, value); }
        }

        private ObservableCollection<StringWrapper> addressesFound;
        public ObservableCollection<StringWrapper> AddressesFound
        {
            get { return addressesFound; }
            set { Set(ref addressesFound, value); }
        }

        private StringWrapper addressSelected;
        public StringWrapper AddressSelected
        {
            get { return addressSelected; }
            set
            {
                Set(ref addressSelected, value);
                if (addressSelected == null)
                {
                    return;
                }

                if (!string.IsNullOrEmpty(addressSelected.StringContent))
                {
                    SearchAddButtonText = "Add";
                }
                else
                {
                    SearchAddButtonText = "Search";
                }
            }
        }

        public ICommand SearchCommand { private set; get; }
        public ICommand CancelCommand { private set; get; }

        private string buildingNumber;
        public string BuildingNumber
        {
            get { return buildingNumber; }
            set
            {
                if (value != buildingNumber)
                {
                    SearchAddButtonText = "Search";
                }

                Set(ref buildingNumber, value);
            }
        }

        private string streetName;
        public string StreetName
        {
            get { return streetName; }
            set
            {
                if (value != streetName)
                {
                    SearchAddButtonText = "Search";
                }

                Set(ref streetName, value);
            }
        }

        private string stopName;
        public string StopName
        {
            get { return stopName; }
            set
            {
                Set(ref stopName, value);
            }
        }

        private string postCode;
        public string PostCode
        {
            get { return postCode; }
            set
            {
                if (value != postCode)
                {
                    SearchAddButtonText = "Search";
                }

                Set(ref postCode, value);
            }
        }

        public SearchAddressViewModel()
        {
            mapService = (App.Current as App).Container.GetService<IMapService>();
            httpService = (App.Current as App).Container.GetService<IHttpService>();
            stopService = (App.Current as App).Container.GetService<IStopService>();

            // New up our observable collection
            AddressesFound = new ObservableCollection<StringWrapper>();

            SearchCommand = new RelayCommand(async () =>
            {
                // If our button is showing "search", let's do the following
                if (SearchAddButtonText == "Search")
                {
                    // Reset everything
                    AddressesFound.Clear();
                    AddressSelected = new StringWrapper();

                    // Will need to do some proper validation here at some point
                    var httpStreetName = StringHelper.GetFormattedStringForHttpRequest(StreetName);

                    // Need to get modified postcode string for http request as well
                    var httpPostcode = StringHelper.GetFormattedStringForHttpRequest(PostCode);

                    // Our string to send
                    var httpStringToSend = BuildingNumber + "+" + httpStreetName + "," + httpPostcode;

                    var response = await httpService.SendGetRequest(httpStringToSend);
                    if (response.Length == 0)
                    {
                        // handle error
                        return;
                    }

                    // Deserialise json we have got back
                    List<RootObject> deserialisedResponses = JsonConvert.DeserializeObject<List<RootObject>>(response);
                    foreach (var responseObtained in deserialisedResponses)
                    {
                        // Sanity check (because we've seen results that don't much the desired postcode
                        if (responseObtained.address == null)
                        {
                            continue;
                        }

                        if (responseObtained.address.postcode != PostCode)
                        {
                            continue;
                        }

                        var stringw = new StringWrapper
                        {
                            StringContent = BuildingNumber + " " + responseObtained.display_name,
                            TempLatitude = decimal.Parse(responseObtained.lat),
                            TempLongitude = decimal.Parse(responseObtained.lon)
                        };
                        AddressesFound.Add(stringw);
                    }
                }
                else // it's basically showing "Add" at this point, so add our item to the list of stops
                {
                    StopModel stopModel = new StopModel() { Name = StopName, Address = AddressSelected.StringContent,
                        Latitude = AddressSelected.TempLatitude, Longitude = AddressSelected.TempLongitude };
                    stopService.AddStop(stopModel);
                }
            });
        }
    }
}
