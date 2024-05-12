using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATMApplication.MVVM.Model
{
	public class Database
	{
		private readonly Dictionary<string, AccountData> accounts;

		public delegate void TransactionDelegate(int accountId, decimal amount, string description);

		public event TransactionDelegate? TransactionEvent;


		public Database()
		{
			this.accounts = [];
			accounts.Add("1234567890123456", new AccountData("1234567890123456", "John Doe", "1234", 1000.0m, "/Images/black_card.png", "#282828", "consentantaneity@gmail.com"));
			accounts.Add("9876543210987654", new AccountData("9876543210987654", "Jane Smith", "4321", 500.0m, "/Images/black_card.png", "#282828", "consentantaneity@gmail.com"));
			accounts.Add("1111111111111111", new AccountData("1111111111111111", "consentantaneity", "1111", 10279.120m, "/Images/purple_card.png", "#042843", "consentantaneity@gmail.com"));
			accounts.Add("2222222222222222", new AccountData("2222222222222222", "consentantaneity's mom xd", "2222", 8943.217m, "/Images/blue_card.png", "#6154FE", "consentantaneity@gmail.com"));
			accounts.Add("", new AccountData("", "", "", 0, "/Images/black_card.png", "#282828", ""));
		}

		public bool IsCardValid(string cardNumber)
		{
			return accounts.ContainsKey(cardNumber);
		}
		public string GetCard(string cardNumber)
		{
			return accounts[cardNumber].CardImage;
		}

		public int SendMoney(string senderCardNumber, string senderPin, string recipientCardNumber, decimal amount)
		{
			if (!IsValidPin(senderCardNumber, senderPin))
			{
				return 0; // Invalid PIN
			}

			if (!IsCardValid(recipientCardNumber))
			{
				return -1;
			}

			decimal senderBalance = GetBalance(senderCardNumber, senderPin);
			if (senderBalance < amount)
			{
				return -2;
			}


			accounts[senderCardNumber].Balance -= amount;
			accounts[recipientCardNumber].Balance += amount;


			accounts[senderCardNumber].AddTransaction(-amount, $"Sent to {recipientCardNumber}");
			accounts[recipientCardNumber].AddTransaction(amount, $"Received from {senderCardNumber}");

			OnTransactionEvent(accounts[senderCardNumber].Id, -amount, $"Sent to {recipientCardNumber}");
			OnTransactionEvent(accounts[recipientCardNumber].Id, amount, $"Received from {senderCardNumber}");

			return 1; // Success
		}

		public decimal GetBalance(string cardNumber, string pin)
		{
			if (!IsValidPin(cardNumber, pin))
			{
				return -1;
			}

			return accounts[cardNumber].Balance;
		}

		public int UpdateBalance(string cardNumber, decimal amount)
		{

			accounts[cardNumber].Balance += amount;

			OnTransactionEvent(accounts[cardNumber].Id, amount, "Переказ на карту");


			accounts[cardNumber].AddTransaction(amount, "Переказ на карту");

			return 1;
		}

		public int AddTransaction(string cardNumber, decimal amount, string description)
		{
			if (accounts.TryGetValue(cardNumber, out AccountData? value))
			{
				OnTransactionEvent(value.Id, amount, description);
				value.AddTransaction(amount, description);

				return 1;
			}

			return 0;
		}

		public string GetName(string cardNumber)
		{
			return accounts[cardNumber].OwnerName;
		}
		public string GetGmail(string cardNumber)
		{
			return accounts[cardNumber].Gmail;
		}

		public string GetColor(string cardNumber)
		{
			return accounts[cardNumber].Color;
		}

		public int WithdrawMoney(string cardNumber, string pin, decimal amount)
		{
			if (!IsValidPin(cardNumber, pin))
			{
				return 0;
			}

			decimal currentBalance = GetBalance(cardNumber, pin);
			if (currentBalance < amount)
			{
				return -1;
			}

			accounts[cardNumber].Balance -= amount;

			OnTransactionEvent(accounts[cardNumber].Id, -amount, "Зняття коштів");


			accounts[cardNumber].AddTransaction(-amount, "Зняття коштів");

			return 1;
		}

		public bool IsValidPin(string cardNumber, string pin)
		{
			if (accounts.TryGetValue(cardNumber, out AccountData? value))
			{
				return value.Pin == pin.Trim();
			}

			return false;
		}

		private void OnTransactionEvent(int accountId, decimal amount, string description)
		{
			TransactionEvent?.Invoke(accountId, amount, description);
		}

		private class AccountData
		{
			private static int nextId = 1;

			public int Id { get; }
			public string CardNumber { get; }
			public string Gmail { get; }
			public string CardImage { get; }
			public string Color { get; }
			public string OwnerName { get; }
			public string Pin { get; }
			public decimal Balance { get; set; }
			public List<Transaction> Transactions { get; }

			public AccountData(string cardNumber, string ownerName, string pin, decimal balance, string cardImage, string color, string gmail)
			{
				Color = color;
				CardImage = cardImage;
				Id = nextId++;
				CardNumber = cardNumber;
				Gmail = gmail;
				OwnerName = ownerName;
				Pin = pin;
				Balance = balance;
				Transactions = new List<Transaction>();
				Gmail = gmail;
			}

			public void AddTransaction(decimal amount, string description)
			{
				Transaction newTransaction = new(amount, description);
				Transactions.Add(newTransaction);
			}
		}

	}
}
