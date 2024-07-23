using Amazon.Library.Models;
using Amazon.Library.Services;
using eCommerce.Library.DTO;
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
        public string? Query { get; set; }
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

        public List<ProductViewModel> Products
        {
            get
            {
                return InventoryServiceProxy.Current.Products.Where(p => p != null)
                    .Select(p => new ProductViewModel(p)).ToList()
                    ?? new List<ProductViewModel>();
            }
        }

        public async void Refresh()
        {
            await InventoryServiceProxy.Current.Get();
            NotifyPropertyChanged(nameof(Products));
        }
       
        public async void EditProduct()
        {
            if (SelectedProduct?.Product == null) return;
            await Shell.Current.GoToAsync($"//Product?ProductId={SelectedProduct.Product.Id}");
            await InventoryServiceProxy.Current.AddOrUpdate(SelectedProduct.Product);
            Refresh();
        }
        public async void DeleteProduct()
        {
            
                if (SelectedProduct?.Product == null) return;
                await InventoryServiceProxy.Current.Delete(SelectedProduct.Product.Id);
                SelectedProduct = null;
                Refresh();
        }
        public event PropertyChangedEventHandler? PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public async void SearchProduct()
        {
            await InventoryServiceProxy.Current.Search(new Query(Query));
            NotifyPropertyChanged(nameof(Products));
        }
    }
}   

