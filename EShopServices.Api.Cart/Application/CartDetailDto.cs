namespace EShopServices.Api.Cart.Application;

public class CartDetailDto
{
    public Guid? BookId { get; set; }
    public string Title { get; set; } = null!;
    public string Author { get; set; } = null!;
    public DateTime? PublishDate { get; set; }

}
