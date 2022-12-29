using EShopServices.Messenger.Email.SendGridLibrary.Interface;
using EShopServices.Messenger.Email.SendGridLibrary.Model;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace EShopServices.Messenger.Email.SendGridLibrary.Implement;

public class MySendGrid : IMySendGrid
{
    public async Task<(bool result, string errorMessage)> SendEmail(SendGridData data)
    {
        try
        {
            var sendGridClient = new SendGridClient(data.SendGridAPIKey);
            var destination = new EmailAddress(data.EmailDestination, data.NameDestination);
            var subject = data.Subject;
            var sender = new EmailAddress("jebarcha@hotmail.com", "jose barajas");
            var content = data.Content;

            var objMessage = MailHelper.CreateSingleEmail(sender, destination, subject, content, content);

            await sendGridClient.SendEmailAsync(objMessage);

            return (true, null);
        }
        catch (Exception ex)
        {
            return (false, ex.Message);
        }
    }
}
