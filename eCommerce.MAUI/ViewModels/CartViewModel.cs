using Amazon.Library.Models;
using Amazon.Library.Services;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace eCommerce.MAUI.ViewModels
{
    class CartViewModel : INotifyPropertyChanged
    {
        private ShoppingCart cart;
        
        public ShoppingCart Cart
        {

            get { return cart; }
            set
            {
                if (cart != value)
                {
                    cart = value;
                    NotifyPropertyChanged();
                    NotifyPropertyChanged(nameof(CartContents));
                    NotifyPropertyChanged(nameof(PriceTotal));


                }
            }
        }

        
        public ObservableCollection<Product> CartContents
        {
            get { return Cart.Contents; }
        }

        public CartViewModel()
        {
            Cart = ShoppingCartService.Current.Cart;
          
            Cart.Contents.CollectionChanged += (s, e) =>
                {
                    NotifyPropertyChanged(nameof(CartContents));
                    NotifyPropertyChanged(nameof(PriceTotal));
                };
            
        }
        
        public event PropertyChangedEventHandler? PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public decimal TaxRate
        {
            get; set;
        }

        public void ApplyTaxRate()
        {
            NotifyPropertyChanged(nameof(PriceTotal));
        }

        public decimal PriceTotal
        {
            get
            {
                decimal total = 0;
                int countBogoPairs = 0;
                bool skipNext = false;

                foreach (var product in Cart.Contents)
                {
                    if (skipNext)
                    {
                        skipNext = false;
                        continue;
                    }

                    if (product.IsBuyOneGetOneFree)
                    {

                        if (countBogoPairs % 2 == 0)
                        {
                            total += product.Price;
                        }

                        countBogoPairs++;
                    }
                    else
                    {
                        total += product.Price;
                    }
                }
                total += total * (TaxRate / 100);
                return total;
            }
        }
        public async void Checkout()
        {
            decimal totalPrice = PriceTotal;
            decimal taxAmount = totalPrice * (TaxRate / 100);
            decimal finalTotal = totalPrice + taxAmount;

            string checkoutMessage = $"Checkout: Price: {totalPrice:C2}, Tax: {taxAmount:C2}, Total: {finalTotal:C2}";

            
            Console.WriteLine(checkoutMessage);

            await App.Current.MainPage.DisplayAlert("Checkout Complete", checkoutMessage, "OK");

            Cart.Contents.Clear();

            NotifyPropertyChanged(nameof(CartContents));
            NotifyPropertyChanged(nameof(PriceTotal));
        }

    }

}
