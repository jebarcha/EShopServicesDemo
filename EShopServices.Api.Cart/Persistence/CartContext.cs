using EShopServices.Api.Cart.Model;
using Microsoft.EntityFrameworkCore;

namespace EShopServices.Api.Cart.Persistence;

public class CartContext : DbContext
{
	public CartContext(DbContextOptions<CartContext> options) : base(options)
	{
	}

	public DbSet<CartSession> CartSession { get; set; } = null!;
	public DbSet<CartSessionDetail> CartSessionDetail { get; set; }
}
