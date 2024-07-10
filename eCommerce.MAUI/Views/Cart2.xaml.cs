using eCommerce.MAUI.ViewModels;

namespace eCommerce.MAUI.Views;

public partial class Cart2 : ContentPage
{
	public Cart2()
	{
        InitializeComponent();
		BindingContext = new Cart2ViewModel();
		

	}
    private void CancelClicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//Shop");
    }

    private void TaxesClicked(object sender, EventArgs e)
    {
        (BindingContext as Cart2ViewModel)?.ApplyTaxRate();
    }
    private void CheckoutClicked(object sender, EventArgs e)
    {
        (BindingContext as Cart2ViewModel)?.Checkout();
        
    }
}