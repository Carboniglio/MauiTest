using Plugin.Fingerprint.Abstractions;
using Plugin.Fingerprint;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MauiTest
{
    internal class LoginViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        public ICommand AuthenticateCommand {get;}

        private string placeholder = "Codice Utente";
        private bool isUsernameRemembered = false;

        public string Placeholder
        {
            get { return placeholder; }
            set
            {
                if (placeholder != value)
                {
                    placeholder = value;
                    OnPropertyChanged(nameof(Placeholder));
                }
            }
        }

        public bool IsUsernameRemembered
        {
            get { return isUsernameRemembered; }
            set
            {
                if (isUsernameRemembered != value)
                {
                    isUsernameRemembered = value;
                    OnPropertyChanged(nameof(IsUsernameRemembered));
                    DataManager.SavePreference<bool>(DataManagerKeys.isPasswordRemembered, value);
                }
            }

        }


        public LoginViewModel()
        {
            AuthenticateCommand = new Command(async() => await AuthenticateAsync());
        }

        //this task is for the biometric login 
        private async  Task AuthenticateAsync()
        {
            //this checks that a biometric login is avaiable 
            var availability = await CrossFingerprint.Current.IsAvailableAsync(true);


            if (!availability)
            {
                // error or fallback
                await Shell.Current.DisplayAlert("Error", "No fingerprint found ", "OK");
                return;
            }

            //this opens the biometric input 
            var config = new AuthenticationRequestConfiguration("Access", "scan your fingerprint")
            {
                AllowAlternativeAuthentication = true,
                FallbackTitle = "Use PIN"
            };

            //this gets the result of the fingerprint/face scan 
            var result = await CrossFingerprint.Current.AuthenticateAsync(config);

            if (result.Authenticated)
            {
                await Shell.Current.DisplayAlert("Done", "Success!", "OK");
                // continue login
            }
            else
            {
                await Shell.Current.DisplayAlert("Failed", "authentication failed.", "OK");
            }
        }



        protected void OnPropertyChanged(string propertyName) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        public void RefreshPlaceholder()
        {
            isUsernameRemembered = DataManager.LoadPreference<bool>(DataManagerKeys.isPasswordRemembered);
            if (isUsernameRemembered)
                Placeholder = DataManager.LoadPreference<string>(DataManagerKeys.Username) ?? "Codice utente";
            else
                Placeholder = "Codice utente";
        }
    }
}
