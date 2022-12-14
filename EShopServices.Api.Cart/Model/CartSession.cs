namespace EShopServices.Api.Cart.Model;

public class CartSession
{
    public int CartSessionId { get; set; }
    public DateTime? CreationDate { get; set; }
    public ICollection<CartSessionDetail> DetailList { get; set; } = null!;
}
