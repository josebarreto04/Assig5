using Amazon.Library.Models;
using Amazon.Library.Services;

using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace eCommerce.MAUI.ViewModels
{
    public class ShopProductViewModel : INotifyPropertyChanged
    {
        private string inventoryList;
 
        public event PropertyChangedEventHandler? PropertyChanged;

        public ShopProductViewModel()
        {
            InventoryList = string.Empty;

        }

        public string InventoryList
        {
            set
            {
                inventoryList = value;
                NotifyPropertyChanged();
                NotifyPropertyChanged(nameof(Products));
            }
            get { return inventoryList; }
        }

        public ProductViewModel? SelectedProduct { get; set; }

        public void AddToCartInShop()
        {

            if (SelectedProduct?.Product == null)
            {
                return;
            }
            if (SelectedProduct.IsBuyOneGetOneFree)
            {
                SelectedProduct.BuyOneGetOneFree();
            }

            ShoppingCartService.Current.AddToCart(SelectedProduct.Product);
            if (SelectedProduct?.Quantity < 1)
            {
                SelectedProduct.Quantity = 0;
                return;
            }
            NotifyPropertyChanged(nameof(Products));
            NotifyPropertyChanged(nameof(PriceTotal));
            NotifyPropertyChanged(nameof(CartContent));

        }
        public void AddToCartInShop2()
        {

            if (SelectedProduct?.Product == null)
            {
                return;
            }
            if (SelectedProduct.IsBuyOneGetOneFree)
            {
                SelectedProduct.BuyOneGetOneFree();
            }

            ShoppingCartService.Current.AddToCart2(SelectedProduct.Product);
            if (SelectedProduct?.Quantity < 1)
            {
                SelectedProduct.Quantity = 0;
                return;
            }
            NotifyPropertyChanged(nameof(Products));
            NotifyPropertyChanged(nameof(PriceTotal));
            NotifyPropertyChanged(nameof(CartContent2));

        }



        public void Refresh()
        {
            NotifyPropertyChanged(nameof(Products));
        }

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public List<ProductViewModel> Products
        {
            get
            {
                return InventoryServiceProxy.Current.Products.Where(p => p != null)
                    .Where(p => p?.Name?.ToUpper()?.Contains(InventoryList.ToUpper()) ?? false)
                    .Select(p => new ProductViewModel(p)).ToList() ?? new List<ProductViewModel>();
            }
        }
        private ShoppingCart cart;

        public ShoppingCart SecondCart
        {
            get { return ShoppingCartService.Current.Cart; }
            set
            {
                cart = value;
                NotifyPropertyChanged();
                NotifyPropertyChanged(nameof(CartContent));
                NotifyPropertyChanged(nameof(PriceTotal));
            }
        }


        public ObservableCollection<Product> CartContent
        {
            get
            {
                return ShoppingCartService.Current.Cart.Contents;
            }
        }
        public ObservableCollection<Product> CartContent2
        {
            get
            {
                return ShoppingCartService.Current.Cart2.Contents;
            }
        }
        public ShoppingCart SecondCart2
        {
            get { return ShoppingCartService.Current.Cart2; }
            set
            {
                cart = value;
                NotifyPropertyChanged();
                NotifyPropertyChanged(nameof(CartContent2));
                NotifyPropertyChanged(nameof(PriceTotal));
            }
        }
        public decimal PriceTotal => cart.Contents?.Sum(c => c.Price) ?? 0;
      

    }
}

