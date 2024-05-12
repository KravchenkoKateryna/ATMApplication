using System;
using System.Windows;

namespace ATMApplication.MVVM.Model
{
	public class TransactionManager
	{
		private Account account;
		private AutomatedTellerMachine atm;
		private Bank bank;
		private string inputCard;
		private string inputAmount;

		public TransactionManager(Account account, AutomatedTellerMachine atm, Bank bank, string inputCard, string inputAmount)
		{
			this.account = account;
			this.atm = atm;
			this.bank = new("Privat24");
			this.inputCard = inputCard;
			this.inputAmount = inputAmount;
		}

		private void SuccessfulOperationHandler(object sender, SuccessfulOperationEventArgs e)
		{
			MessageBox.Show($"Операція успішна: {e.Operator}{e.Parameter}.\nВідправлено на почту: {e.Gmail}");
		}
		private void SubscribeToSuccessfulOperationEvent()
		{
			bank.SuccessfulOperation += SuccessfulOperationHandler;
		}

		public void TopUpMoney(object parameter)
		{
			account.Balance += Convert.ToDecimal(parameter);
			atm.MoneyAmount += Convert.ToDecimal(parameter);
			bank.SendMessage(parameter.ToString(), "+", account.GmailAddress);
			account.Transactions.Add(new Transaction(+Convert.ToDecimal(parameter), $"{atm.ATMId} Поповнення карти"));
			SubscribeToSuccessfulOperationEvent();
		}

		private void PerformTransaction(decimal amount, string transactionType)
		{
			try
			{
				decimal balance = account.Balance;

				if (balance < amount)
				{
					MessageBox.Show("Недостатньо коштів.");
					return;
				}

				account.Balance -= amount;
				account.Transactions.Add(new Transaction(-amount, $"{atm.ATMId} {transactionType}"));
				bank.SendMessage(amount.ToString(), transactionType == "Зняття коштів" ? "-" : "+", account.GmailAddress);
				SubscribeToSuccessfulOperationEvent();
			}
			catch (Exception ex)
			{
				MessageBox.Show($"Помилка при здійсненні операції: {ex.Message}");
			}
		}

		public void SendMoney(object parameter)
		{
			if (account.CardNumber == inputCard)
			{
				MessageBox.Show("🚫 Це ваша картка!");
				return;
			}

			Database database = new ();

			if (!database.IsCardValid(inputCard))
			{
				MessageBox.Show("🚫 Неправильний номер карти");
				return;
			}

			decimal amount = Convert.ToDecimal(parameter);
			PerformTransaction(amount, "Переказ на карту");
		}

		public void WithdrawMoney(object parameter)
		{
			decimal amount = Convert.ToDecimal(parameter);

			if (atm.MoneyAmount < amount)
			{
				MessageBox.Show("В терміналі недостатньо коштів.\nОперація не виконана");
				return;
			}

			PerformTransaction(amount, "Зняття коштів");
		}
	}
}
