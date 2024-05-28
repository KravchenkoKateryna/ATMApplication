using ATMApplication.MVVM.Model;
using System.ComponentModel;
using System.Windows;

namespace ATMApplication.MVVM.ViewModel
{
    public class AuthViewModel : INotifyPropertyChanged
    {
        private string _inputText = string.Empty;
        private string _inputPin = string.Empty;
        private BalanceViewModel account;

        public AuthViewModel()
        {
            account = new BalanceViewModel(this);
        }

        public int SaveUserData()
        {
            Database database = new();

            if (!database.IsCardValid(account.Account.CardNumber))
            {
                MessageBox.Show("🚫 Неправильний номер карти");
                return 0;
            }

            if (!database.IsValidPin(account.Account.CardNumber, account.Account.Pin))
            {
                MessageBox.Show("🚫 Неправильний пін карти");
                return 0;
            }

            return 1;
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }

        public string InputText
        {
            get { return _inputText; }
            set
            {
                if (_inputText != value)
                {
                    _inputText = value;
                    OnPropertyChanged(nameof(InputText));
                    account.Account.CardNumber = value;
                }
            }
        }

        public string InputPin
        {
            get { return _inputPin; }
            set
            {
                if (_inputPin != value)
                {
                    _inputPin = value;
                    OnPropertyChanged(nameof(InputPin));
                    account.Account.Pin = value;
                }
            }
        }
    }
}
