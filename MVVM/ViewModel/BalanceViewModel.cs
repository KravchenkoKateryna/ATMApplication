using ATMApplication.Core;
using ATMApplication.MVVM.Model;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace ATMApplication.MVVM.ViewModel
{
    class BalanceViewModel : INotifyPropertyChanged
    {
        private Account account;

        AutomatedTellerMachine atm;
        private Bank _Bank;
        private readonly TransactionManager _transactionManager;

        private RelayCommand _copyCardNumberCommand;
        private RelayCommand _withdrawBalance;
        private RelayCommand _topupBalance;
        private RelayCommand _transferBalance;
        private string _inputText = string.Empty;
        private string inputAmount = string.Empty;

        public string InputCard
        {
            get { return _inputText; }
            set
            {
                if (_inputText != value)
                {
                    _inputText = value;
                    OnPropertyChanged(nameof(InputCard));
                }
            }
        }

        public string InputAmount
        {
            get { return inputAmount; }
            set
            {
                if (inputAmount != value)
                {
                    inputAmount = value;
                    OnPropertyChanged(nameof(InputCard));
                }
            }
        }

        public ICommand CopyCardNumberCommand
        {
            get
            {
                _copyCardNumberCommand ??= new RelayCommand(CopyCardNumber);
                return _copyCardNumberCommand;
            }
        }

        private void CopyCardNumber(object parameter)
        {
            var cardNumber = Account.CardNumber;
            Clipboard.SetText(cardNumber);
        }

        public ICommand WithdrawBalance
        {
            get
            {
                _withdrawBalance ??= new RelayCommand(_transactionManager.WithdrawMoney);
                return _withdrawBalance;
            }
        }

        public ICommand TransferBalance
        {
            get
            {
                _transferBalance ??= new RelayCommand(_transactionManager.SendMoney);
                return _transferBalance;
            }
        }

        public ICommand TopUpBalance
        {
            get
            {
                _topupBalance ??= new RelayCommand(_transactionManager.TopUpMoney);
                return _topupBalance;
            }
        }

        public BalanceViewModel()
        {
            if (account == null)
            {
                atm = new AutomatedTellerMachine("ATM001", 10000, "Велика Бердичівська 13", "XYZ Corp", "ATM Model 123", "v1.0");
                account = new Account("", "");
                Bank = new("Private24");
            }
        }


        public BalanceViewModel(AuthViewModel authViewModel)
        {
            _Bank = new Bank("Tantanbank");
            account = new Account(authViewModel.InputText, authViewModel.InputPin);
            atm = new AutomatedTellerMachine("ATM001", 10000, "Велика Бердичівська 13", "XYZ Corp", "ATM Model 123", "v1.0");
            _transactionManager = new TransactionManager(account, atm, _Bank, InputCard, InputAmount);
        }

        public Account Account
        {
            get { return account; }
            set
            {
                if (account != value)
                {
                    account = value;
                    OnPropertyChanged(nameof(Account));
                }
            }
        }

        public AutomatedTellerMachine ATM
        {
            get { return atm; }
            set
            {
                if (atm != value)
                {
                    atm = value;
                    OnPropertyChanged(nameof(ATM));
                }
            }
        }


        public Bank Bank
        {
            get { return _Bank; }
            set
            {
                if (_Bank != value)
                {
                    _Bank = value;
                    OnPropertyChanged(nameof(Bank));
                }
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
}
