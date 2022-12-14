using EShopServices.Api.Book.Model;
using Microsoft.EntityFrameworkCore;

namespace EShopServices.Api.Book.Persistence;

public class ContextBookstore : DbContext
{
	public ContextBookstore() { }
	public ContextBookstore(DbContextOptions<ContextBookstore> options) : base(options)
	{
	}

	public virtual DbSet<Model.MaterialBook> MaterialLibrary { get; set; }

}
