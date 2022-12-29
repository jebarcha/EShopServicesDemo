namespace EShopServices.Messenger.Email.SendGridLibrary.Model;

public class SendGridData
{
    public string SendGridAPIKey { get; set; }
    public string EmailDestination { get; set; }
    public string NameDestination { get; set; }
    public string Subject { get; set; }
    public string Content { get; set; }
}
