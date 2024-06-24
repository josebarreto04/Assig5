using Amazon.Library.Models;
using eCommerce.MAUI.ViewModels;

namespace eCommerce.MAUI.Views;

public partial class ShopView : ContentPage
{
   
    public ShopView()
	{
		InitializeComponent();
        BindingContext = new ShopProductViewModel();
	}

    private void CancelClicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//MainPage");
    }

   
    private void ShopCartClicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//AddShop");
    }
    private void AddToCartClicked(object sender, EventArgs e)
    {
        (BindingContext as ShopProductViewModel)?.AddToCartInShop();
    }
    private void ContentPage_NavigatedTo(object sender, NavigatedToEventArgs e)
    {
        (BindingContext as ShopProductViewModel)?.Refresh();
    }
    protected override void OnAppearing()
    {
        base.OnAppearing();
        (BindingContext as ShopProductViewModel)?.Refresh();
    }
}