using System.ComponentModel.DataAnnotations;

namespace EShopServices.Api.Author.Model;

public class AcademicGrade
{
    [Key]
    public int AcademidGradeId { get; set; }

    public string Name { get; set; } = null!;
    public string AcademicCenter { get; set; } = null!;
    public DateTime? GradeDate { get; set; }
    public int AuthorBookId { get; set; }
    public AuthorBook AuthorBook { get; set; } = null!;
    public string AcademicGradeGuid { get; set; } = null!;
}
