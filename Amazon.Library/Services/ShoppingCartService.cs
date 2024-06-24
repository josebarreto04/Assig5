using Amazon.Library.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amazon.Library.Services
{
    public class ShoppingCartService
    {
      
        private static ShoppingCartService? instance;
        private static object instanceLock = new object();

        private ObservableCollection<ShoppingCart> carts = new ObservableCollection<ShoppingCart>();

        public ShoppingCart Cart
        {
            get
            {
                if(carts == null || !carts.Any())
                {
                    var newCart= new ShoppingCart() { Id=1, Contents= new ObservableCollection<Product>() };
                    carts.Add(newCart);
                    return newCart;
                }
                return carts.First();
            }
        }

        private ShoppingCartService() { }

        public static ShoppingCartService Current
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new ShoppingCartService();
                    }
                }
                return instance;
            }
        }

        public void AddToCart(Product newProduct)
        {
            if (newProduct == null) return;
            var existingProduct= Cart.Contents.FirstOrDefault(p=>p.Id==newProduct.Id);
            var inventoryProduct = InventoryServiceProxy.Current.Products.FirstOrDefault(p => p.Id == newProduct.Id);
            if (inventoryProduct == null || inventoryProduct.Quantity < 1)
            {
                return;
            }
            Cart.Contents.Add(newProduct);
            --inventoryProduct.Quantity;
        }

        public void DeleteFromCart(Product productToDelete)
        {
            if (Cart?.Contents == null)
            {
                return;
            }

            var existingProduct = Cart.Contents
                .FirstOrDefault(existingProducts => existingProducts.Id == productToDelete.Id);

            if (existingProduct != null)
            {
                var inventoryProduct = InventoryServiceProxy.Current.Products.FirstOrDefault(invProd => invProd.Id == productToDelete.Id);
                if (inventoryProduct != null)
                {
                    inventoryProduct.Quantity += existingProduct.Quantity;
                }

                Cart.Contents.Remove(existingProduct);
            }
        }
    }

}

