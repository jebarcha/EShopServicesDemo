namespace EShopServices.Api.Cart.RemoteModel;

public class RemoteBook
{
    public Guid? MaterialLibraryId { get; set; }
    public string Title { get; set; } = null!;
    public DateTime? PublishDate { get; set; }
    public Guid? AuthorBookId { get; set; }
}
