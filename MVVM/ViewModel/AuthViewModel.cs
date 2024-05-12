using ATMApplication.Core;
using ATMApplication.MVVM.Model;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Security.Principal;
using System.Windows;
using System.Windows.Data;

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

			if (database.IsCardValid(account.Account.CardNumber))
			{
				if (!database.IsValidPin(account.Account.CardNumber, account.Account.Pin))
				{
					MessageBox.Show("🚫 Неправильний пін карти");
					return 0;
				}
				else
				{
					return 1;
				}
			}
			else
			{
				MessageBox.Show("🚫 Неправильний номер карти");
				return 0;
			}
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
