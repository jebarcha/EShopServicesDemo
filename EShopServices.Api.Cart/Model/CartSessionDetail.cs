namespace EShopServices.Api.Cart.Model;

public class CartSessionDetail
{
    public int CartSessionDetailId { get; set; }
    public DateTime? CreationDate { get; set; }
    public string SelectedProduct { get; set; } = null!;
    
    public int CartSessionId { get; set; }
    public CartSession CartSession { get; set; } = null!;
}
