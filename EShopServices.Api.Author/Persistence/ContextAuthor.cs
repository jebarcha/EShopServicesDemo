using EShopServices.Api.Author.Model;
using Microsoft.EntityFrameworkCore;

namespace EShopServices.Api.Author.Persistence;

public class ContextAuthor : DbContext
{
	public ContextAuthor(DbContextOptions<ContextAuthor> options) : base(options) { }

	public DbSet<AuthorBook> AuthorBook { get; set;  }
    public DbSet<AcademicGrade> AcademicGrade { get; set; }
}
