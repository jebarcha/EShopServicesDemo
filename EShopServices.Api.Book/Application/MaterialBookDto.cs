namespace EShopServices.Api.Book.Application;

public class MaterialBookDto
{
    public Guid? MaterialLibraryId { get; set; }
    public string Title { get; set; } = null!;
    public DateTime? PublishDate { get; set; }
    public Guid? AuthorBookId { get; set; }
}
