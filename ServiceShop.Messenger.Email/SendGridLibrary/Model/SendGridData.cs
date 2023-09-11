namespace ServiceShop.Messenger.Email.SendGridLibrary.Model
{
    public class SendGridData
    {
        public string SendGridAPIKey { get; set; }
        public string To { get; set; }
        public string Name { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
    }
}
