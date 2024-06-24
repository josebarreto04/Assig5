using Amazon.Library.Models;
using Amazon.Library.Services;
using eCommerce.MAUI.ViewModels;

namespace eCommerce.MAUI.Views;
[QueryProperty(nameof(ProductId), "ProductId")]
public partial class ProductView : ContentPage
{
    public int ProductId { get; set; }
    public ProductView()
	{
		InitializeComponent();
	}

    public Product? SelectedProduct { get; set; }
 
    private void CancelClicked(object sender, EventArgs e)
    {
		Shell.Current.GoToAsync("//Inventory");
    }
   
    private void AddClicked(object sender, EventArgs e)
    {
        (BindingContext as ProductViewModel)?.Add();
        Shell.Current.GoToAsync("//Inventory");
    }
    private void ContentPage_NavigatedTo(object sender, NavigatedToEventArgs e)
    {
        BindingContext = new ProductViewModel(ProductId);
    }
}