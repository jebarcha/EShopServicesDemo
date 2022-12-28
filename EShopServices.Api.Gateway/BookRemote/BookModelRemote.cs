namespace EShopServices.Api.Gateway.BookRemote;

public class BookModelRemote
{
    public Guid? MaterialLibraryId { get; set; }
    public string Title { get; set; } = null!;
    public DateTime? PublishDate { get; set; }
    public Guid? AuthorBookId { get; set; }
    public AuthorModelRemote AuthorData { get; set; } = null!;
}
