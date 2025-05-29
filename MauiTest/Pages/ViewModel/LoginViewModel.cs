using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiTest
{
    internal class LoginViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;


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
                    DataManager.SavePreference<bool>(DataManagerKeys.isPasswordRemembered.ToString(), value);
                }
            }

        }

        protected void OnPropertyChanged(string propertyName) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));




        public void RefreshPlaceholder()
        {
            isUsernameRemembered = DataManager.LoadPreference<bool>(DataManagerKeys.isPasswordRemembered.ToString());
            if (isUsernameRemembered)
                Placeholder = DataManager.LoadPreference<string>(DataManagerKeys.Username.ToString()) ?? "Codice utente";
            else
                Placeholder = "Codice utente";
        }
    }
}
