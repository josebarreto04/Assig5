using Amazon.Library.Models;
using eCommerce.MAUI.ViewModels;
namespace eCommerce.MAUI.Views;

public partial class ShopProduct : ContentPage
{
    
    public ShopProduct()
    {
        InitializeComponent();
        BindingContext = new CartViewModel();
    }
    private void CancelClicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//Shop");
    }

    private void CheckoutFromCart(object sender, EventArgs e)
    {

    }
}
