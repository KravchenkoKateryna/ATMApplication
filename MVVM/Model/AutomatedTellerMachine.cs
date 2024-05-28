namespace ATMApplication.MVVM.Model
{
    public class AutomatedTellerMachine(string atmId, decimal initialMoneyAmount, string address, string manufacturer, string model, string softwareVersion)
    {
        public decimal MoneyAmount { get; set; } = initialMoneyAmount;
        public string ATMId { get; set; } = atmId;
        public string Address { get; set; } = address;
        public string Manufacturer { get; set; } = manufacturer;
        public string Model { get; set; } = model;
        public string SoftwareVersion { get; set; } = softwareVersion;
    }
}
