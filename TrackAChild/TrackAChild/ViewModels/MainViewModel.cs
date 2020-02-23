using System;
using System.Diagnostics;
using System.Windows.Input;
using TrackAChild.Helpers;
using TrackAChild.Models;
using TrackAChild.Services;
using TrackAChild.Views;
using Windows.UI.Xaml.Media;

namespace TrackAChild.ViewModels
{
    public class MainViewModel : Observable
    {
        public ICommand PassportSignInCommand { get; set; }
        public ICommand RegisterPointerPressedCommand { get; set; }
        public ICommand NavigatedToCommand { get; set; }

        private UserModel userModel;

        private SolidColorBrush passportStatusBackground = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 34, 177, 76));
        public SolidColorBrush PassportStatusBackground
        {
            get { return passportStatusBackground; }
            set { Set(ref passportStatusBackground, value); }
        }

        private string errorMessageText = "";
        public string ErrorMessageText
        {
            get { return errorMessageText; }
            set { Set(ref errorMessageText, value); }
        }

        private string usernameText = "";
        public string UsernameText
        {
            get { return usernameText; }
            set { Set(ref usernameText, value); }
        }

        private string passportStatusText = "Microsoft Passport is ready to use!";
        public string PassportStatusText
        {
            get { return passportStatusText; }
            set { Set(ref passportStatusText, value); }
        }

        private bool isPassportSignInEnabled = true;
        public bool IsPassportSignInEnabled
        {
            get { return isPassportSignInEnabled; }
            set { Set(ref isPassportSignInEnabled, value); }
        }

        public MainViewModel()
        {
            PassportSignInCommand = new RelayCommand(() =>
            {
                ErrorMessageText = "";
                SignInPassport();
            });

            NavigatedToCommand = new RelayCommand(async () =>
            {
                // Check Microsoft Passport is setup and available on this machine
                if (await MicrosoftPassportHelper.MicrosoftPassportAvailableCheckAsync())
                {
                }
                else
                {
                    // Microsoft Passport is not setup so inform the user
                    PassportStatusBackground = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 50, 170, 207));
                    PassportStatusText = "Microsoft Passport is not setup!\n" +
                        "Please go to Windows Settings and set up a PIN to use it.";
                    IsPassportSignInEnabled = false;
                }
            });

            RegisterPointerPressedCommand = new RelayCommand<object>((param) =>
            {
                ErrorMessageText = "";
            });
        }

        /// <summary>
        /// Try to sign in to Microsoft Passport for authentication
        /// </summary>
        private async void SignInPassport()
        {
            if (AccountHelper.ValidateAccountCredentials(UsernameText))
            {
                // Create and add a new local account
                userModel = AccountHelper.AddAccount(UsernameText);
                Debug.WriteLine("Successfully signed in with traditional credentials and created local account instance!");

                if (await MicrosoftPassportHelper.CreatePassportKeyAsync(UsernameText))
                {
                    Debug.WriteLine("Successfully signed in with Microsoft Passport!");

                    // Since we go into here for the time being, lets navigate to the Map page
                    NavigationService.Navigate(typeof(MapViewPage));
                }
            }
            else
            {
                ErrorMessageText = "Invalid Credentials";
            }
        }
    }
}
