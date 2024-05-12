using ATMApplication.MVVM.Model.Templates;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATMApplication.MVVM.Model
{
	public class Account : INotifyPropertyChanged, IAccount
	{
		public event PropertyChangedEventHandler PropertyChanged;

		private string _cardNumber;
		private string _gmailAddress;
		private string _pin;
		private string _name;
		private string _balanceText;
		private decimal _balance;
		private string _cardNumberView;
		private string _cardImage;
		private string _color;
		private ObservableCollection<Transaction> _transactions;

		public DateTime LastLoginTime { get; set; }

		public string CardNumber
		{
			get => _cardNumber;
			set { SetProperty(ref _cardNumber, value, nameof(CardNumber)); }
		}

		public string GmailAddress
		{
			get => _gmailAddress;
			set { SetProperty(ref _gmailAddress, value, nameof(GmailAddress)); }
		}

		public string CardImage
		{
			get => _cardImage;
			set { SetProperty(ref _cardImage, value, nameof(CardImage)); }
		}

		public string ColorGradient
		{
			get => _color;
			set { SetProperty(ref _color, value, nameof(ColorGradient)); }
		}

		public string CardNumberView
		{
			get => _cardNumberView;
			set { SetProperty(ref _cardNumberView, value, nameof(CardNumberView)); }
		}

		public string Pin
		{
			get => _pin;
			set { SetProperty(ref _pin, value, nameof(Pin)); }
		}

		public string Name
		{
			get => _name;
			set { SetProperty(ref _name, value, nameof(Name)); }
		}

		public string BalanceText
		{
			get => _balanceText;
			set { SetProperty(ref _balanceText, value, nameof(BalanceText)); }
		}

		public decimal Balance
		{
			get => _balance;
			set { SetProperty(ref _balance, value, nameof(Balance)); }
		}

		public ObservableCollection<Transaction> Transactions
		{
			get => _transactions;
			set { SetProperty(ref _transactions, value, nameof(Transactions)); }
		}

		public Account(string cardNumber, string pin)
		{
			Database database = new Database();

			CardNumber = cardNumber;
			Pin = pin;
			Name = database.GetName(CardNumber);
			GmailAddress = database.GetGmail(CardNumber);
			BalanceText = $"{database.GetBalance(cardNumber, pin)} ₴";
			Transactions = new ObservableCollection<Transaction>();
			Balance = database.GetBalance(cardNumber, pin);
			CardImage = database.GetCard(cardNumber);
			ColorGradient = database.GetColor(cardNumber);
			LastLoginTime = DateTime.Now;

			CardNumberView = cardNumber.Length >= 16 ?
				$"{cardNumber.Substring(0, 4)} - {cardNumber.Substring(4, 4)} - {cardNumber.Substring(8, 4)} - {cardNumber.Substring(12, 4)}" :
				"Invalid Card Number";
		}
		protected virtual void OnPropertyChanged(string propName)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
		}

		private void SetProperty<T>(ref T field, T value, string propertyName)
		{
			if (!EqualityComparer<T>.Default.Equals(field, value))
			{
				field = value;
				OnPropertyChanged(propertyName);
			}
		}
	}


}
