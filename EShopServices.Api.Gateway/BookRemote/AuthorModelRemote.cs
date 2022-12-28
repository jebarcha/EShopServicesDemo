namespace EShopServices.Api.Gateway.BookRemote;

public class AuthorModelRemote
{
    public string Name { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public DateTime? Birthday { get; set; }
    public string AuthorBookGuid { get; set; } = null!;
}
