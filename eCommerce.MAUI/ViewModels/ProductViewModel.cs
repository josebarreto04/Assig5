using Amazon.Library.Models;
using Amazon.Library.Services;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;


namespace eCommerce.MAUI.ViewModels
{

    public class ProductViewModel : INotifyPropertyChanged
    {
        private Product? product;
        public Product? Product
        {
            get { return product; }
            set
            {
                if (product != value)
                {
                    product = value;
                    NotifyPropertyChanged();
                    NotifyPropertyChanged(nameof(Name));
                    NotifyPropertyChanged(nameof(Description));
                    NotifyPropertyChanged(nameof(Id));
                    NotifyPropertyChanged(nameof(Quantity));
                    NotifyPropertyChanged(nameof(DisplayPrice));
                    NotifyPropertyChanged(nameof(PriceAsString));
                    NotifyPropertyChanged(nameof(IsBuyOneGetOneFree));
                    NotifyPropertyChanged(nameof(Discount));



                }
            }
        }
        public bool IsBuyOneGetOneFree
        {
            get { return Product?.IsBuyOneGetOneFree ?? false; }
            set
            {
                if (Product != null && Product.IsBuyOneGetOneFree != value)
                {
                    Product.IsBuyOneGetOneFree = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public string? Name
        {
            get { return Product?.Name ?? string.Empty; }
            set
            {
                if (Product != null && Product.Name != value)
                {
                    Product.Name = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public decimal Discount
        {
            get { return Product?.Discount ?? 0; }
            set
            {
                if (Product != null && Product.Discount != value)
                {
                    Product.Discount = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public string? Description
        {
            get { return Product?.Description ?? string.Empty; }
            set
            {
                if (Product != null && Product.Description != value)
                {
                    Product.Description = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public int Id
        {
            get { return Product?.Id ?? 0; }
            set
            {
                if (Product != null && Product.Id != value)
                {
                    Product.Id = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public int Quantity
        {
            get { return Product?.Quantity ?? 0; }
            set
            {
                if (Product != null && Product.Quantity != value)
                {
                    Product.Quantity = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public string DisplayPrice
        {
            get
            {
                if (Product == null) { return string.Empty; }
                return $"{Product.Price:C}";
            }
        }
        public decimal Price
        {

            get { return Product?.Price ?? 0; }
            set
            {
                if (Product != null && Product.Price != value)
                {
                    Product.Price = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public ICommand? EditCommand { get; private set; }
        public ICommand? AddCommand { get; private set; }

        public ICommand? DeleteCommand { get; private set; }

        public ProductViewModel()
        {
            Product = new Product();
            SetupCommands();
        }
        public ProductViewModel(int id)
        {
            Product = InventoryServiceProxy.Current?.Products?.FirstOrDefault(p => p.Id == id);

            SetupCommands();
        }
        public ProductViewModel(Product p)
        {

            Product = p ?? new Product();
            SetupCommands();
        }
        public event PropertyChangedEventHandler? PropertyChanged;
        protected void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private void ExecuteEdit(ProductViewModel? p)
        {
            if (p?.Product == null)
            {
                return;
            }
            Shell.Current.GoToAsync($"//Product?ProductId={p.Product.Id}");
        }
        private void ExecuteAdd(ProductViewModel? p)
        {
            if (p?.Product == null)
            {
                return;
            }
            ShoppingCartService.Current.AddToCart(p.Product);
            NotifyPropertyChanged(nameof(Product));
        }
        private void ExecuteDelete(ProductViewModel? p)
        {
            if (p?.Product == null)
            {
                return;
            }
            ShoppingCartService.Current.DeleteFromCart(p.Product);
            NotifyPropertyChanged(nameof(Product));

        }
        public override string ToString()
        {
            if (Product == null)
            {
                return string.Empty;
            }
            return $"{Product.Id} - {Product.Name} - {Product.Price:C}";
        }



        public void SetupCommands()
        {
            EditCommand = new Command((p) => ExecuteEdit(p as ProductViewModel));
            AddCommand = new Command((p) => ExecuteAdd(p as ProductViewModel));
            DeleteCommand = new Command((p) => ExecuteAdd(p as ProductViewModel));

        }


        public string PriceAsString
        {
            set
            {
                if (Product == null)
                {
                    return;
                }
                if (decimal.TryParse(value, out var price))
                {
                    Product.Price = price;
                }
            }
        }
        public void Add()
        {
            if (Product != null)
            {
                InventoryServiceProxy.Current.AddOrUpdate(Product);
            }
        }

        public void BuyOneGetOneFree()
        {
            if (Product != null)
            {
                NotifyPropertyChanged(nameof(Quantity));
            }

        }
     
        public void ApplyMarkDown()
        {

            if (Product != null)
            {
                if (Discount < 0)
                {
                    Discount = 0;
                }

                else if (Discount > 100)
                {
                    Discount = 100;
                }
                decimal discountedPrice = Product.Price * (1 - (Discount / 100));
                Product.Price = discountedPrice;
                NotifyPropertyChanged(nameof(Product)); // Notify changes in the Product object
                NotifyPropertyChanged(nameof(PriceAsString)); // Notify changes in the price representation
            }
        }
    }
}
