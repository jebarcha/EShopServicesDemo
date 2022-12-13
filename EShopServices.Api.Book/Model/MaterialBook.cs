using System.ComponentModel.DataAnnotations;

namespace EShopServices.Api.Book.Model;

public class MaterialBook
{
    [Key]
    public Guid? MaterialLibraryId { get; set; }
    public string Title { get; set; } = null!;
    public DateTime? PublishDate { get; set; }
    public Guid? AuthorBookId { get; set; }
}
