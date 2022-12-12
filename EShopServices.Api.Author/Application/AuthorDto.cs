namespace EShopServices.Api.Author.Application;

public class AuthorDto
{
    public string Name { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public DateTime? Birthday { get; set; }
    public string AuthorBookGuid { get; set; } = null!;
}
