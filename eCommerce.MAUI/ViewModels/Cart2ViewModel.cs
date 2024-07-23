using Amazon.Library.Models;
using Amazon.Library.Services;
using eCommerce.Library.DTO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.MAUI.ViewModels
{
     public class Cart2ViewModel : INotifyPropertyChanged
    {
        private ShoppingCart carttwo;

        public ShoppingCart CartTwo
        {

            get { return carttwo; }
            set
            {
                if (carttwo != value)
                {
                    carttwo = value;
                    NotifyPropertyChanged();
                    NotifyPropertyChanged(nameof(CartContentsTwo));
                    NotifyPropertyChanged(nameof(PriceTotal));


                }
            }
        }


        public ObservableCollection<ProductDTO> CartContentsTwo
        {
            get { return CartTwo.Contents2; }
        }

        public Cart2ViewModel()
        {
            CartTwo = ShoppingCartService.Current.Cart2;

            CartTwo.Contents2.CollectionChanged += (s, e) =>
            {
                NotifyPropertyChanged(nameof(CartContentsTwo));
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

                foreach (var product in CartTwo.Contents2)
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

            // Display the checkout details in the console
            Console.WriteLine(checkoutMessage);

            // Display alert to notify user
            await App.Current.MainPage.DisplayAlert("Checkout Complete", checkoutMessage, "OK");

            // Clear cart contents after checkout (example: simulate clearing cart)
            CartTwo.Contents2.Clear();

            // Notify UI of changes after checkout
            NotifyPropertyChanged(nameof(CartContentsTwo));
            NotifyPropertyChanged(nameof(PriceTotal));
        }
        
    }


    }


