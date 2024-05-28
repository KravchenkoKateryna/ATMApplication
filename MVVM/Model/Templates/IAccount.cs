using System.Collections.ObjectModel;

namespace ATMApplication.MVVM.Model.Templates
{
    public interface IAccount
    {
        string CardNumber { get; set; }
        string GmailAddress { get; set; }
        string CardImage { get; set; }
        string ColorGradient { get; set; }
        string CardNumberView { get; set; }
        string Pin { get; set; }
        string Name { get; set; }
        string BalanceText { get; set; }
        decimal Balance { get; set; }
        ObservableCollection<Transaction> Transactions { get; set; }
        DateTime LastLoginTime { get; set; }
    }
}
