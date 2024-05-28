namespace ATMApplication.MVVM.Model
{
    public class Transaction(decimal amount, string description)
    {
        public decimal Amount { get; set; } = amount;
        public string Description { get; set; } = description;
        public string TransText { get; set; } = $"{amount} ₴";
        public DateTime Timestamp { get; set; } = DateTime.Now;
    }
}
