using Amazon.Library.Models;
using Amazon.Library.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace eCommerce.MAUI.ViewModels
{
    public class InventoryViewModel : INotifyPropertyChanged
    {
        private ProductViewModel? selectedProduct;

        public ProductViewModel? SelectedProduct
        {
            get => selectedProduct;
            set
            {
                if (selectedProduct != value)
                {
                    selectedProduct = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public List<ProductViewModel> Products => InventoryServiceProxy.Current.Products
            .Where(p => p != null)
            .Select(p => new ProductViewModel(p))
            .ToList()
            ?? new List<ProductViewModel>();

        public void Refresh()
        {
            NotifyPropertyChanged(nameof(Products));
        }

        public void EditProduct()
        {
            if (SelectedProduct?.Product == null) return;
            Shell.Current.GoToAsync($"//Product?ProductId={SelectedProduct.Product.Id}");
            InventoryServiceProxy.Current.AddOrUpdate(SelectedProduct.Product);
        }
        public void AddProduct()
        {
            var newProduct = new Product();
            InventoryServiceProxy.Current.AddOrUpdate(newProduct);
            Shell.Current.GoToAsync($"//Product?ProductId={newProduct.Id}");
            Refresh();
        }
        public void DeleteProduct()
        {
            
                if (SelectedProduct?.Product == null) return;
                InventoryServiceProxy.Current.Delete(SelectedProduct.Product.Id);
                SelectedProduct = null;
                Refresh();
        }
        public event PropertyChangedEventHandler? PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}   

