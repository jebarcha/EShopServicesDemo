using EShopServices.Api.Book.Model;
using Microsoft.EntityFrameworkCore;

namespace EShopServices.Api.Book.Persistence;

public class ContextBookstore : DbContext
{
	public ContextBookstore(DbContextOptions<ContextBookstore> options) : base(options)
	{
	}

	public DbSet<Model.MaterialBook> MaterialLibrary { get; set; }

}
