namespace ATMApplication.MVVM.Model
{
    public delegate void SuccessfulOperationEventHandler(object sender, SuccessfulOperationEventArgs e);

    public class SuccessfulOperationEventArgs(string parameter, string _operator, string gmail) : EventArgs
    {
        public string Parameter { get; } = parameter;
        public string Operator { get; } = _operator;
        public string Gmail { get; } = gmail;
    }

    public class Bank(string bankName)
    {
        private string _bankName = bankName;
        private List<AutomatedTellerMachine> atms = [];
        private readonly Database _database = new();

        public event SuccessfulOperationEventHandler? SuccessfulOperation;

        public List<AutomatedTellerMachine> ATMs
        {
            get { return atms; }
            set { atms = value; }
        }

        public Database GetDatabase()
        {
            return _database;
        }

        public void SendMessage(string parameter, string _operator, string gmail)
        {
            //string sender = ""; 
            //string pass = ""; 

            //var smtpClient = new SmtpClient("smtp.gmail.com")
            //{
            //	Port = 587,
            //	UseDefaultCredentials = false,
            //	Credentials = new NetworkCredential(sender, pass),
            //	EnableSsl = true,
            //	DeliveryMethod = SmtpDeliveryMethod.Network
            //};

            //smtpClient.Send(sender, gmail, "✅", $"{_operator}{parameter} UAH");

            OnSuccessfulOperation(new SuccessfulOperationEventArgs(parameter, _operator, gmail));
        }

        protected virtual void OnSuccessfulOperation(SuccessfulOperationEventArgs e)
        {
            SuccessfulOperation?.Invoke(this, e);
        }
    }
}
