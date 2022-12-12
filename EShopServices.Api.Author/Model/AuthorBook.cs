using System.ComponentModel.DataAnnotations;

namespace EShopServices.Api.Author.Model;

public class AuthorBook
{
    [Key]
    public int AuthorBookId { get; set; }
    
    public string Name { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public DateTime? Birthday { get; set; }
    public ICollection<AcademicGrade> AcademicGradeList { get; set; } = null!;
    public string AuthorBookGuid { get; set; } = null!;
}
