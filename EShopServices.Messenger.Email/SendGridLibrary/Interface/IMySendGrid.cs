using EShopServices.Messenger.Email.SendGridLibrary.Model;

namespace EShopServices.Messenger.Email.SendGridLibrary.Interface;

public interface IMySendGrid
{
    Task<(bool result, string errorMessage)> SendEmail(SendGridData data);
}
