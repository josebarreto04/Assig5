using Amazon.Library.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amazon.Library.Services
{
    public class InventoryServiceProxy
    {
        private static InventoryServiceProxy? instance;
        private static object instanceLock = new object();

        private List<Product> products;

        public ReadOnlyCollection<Product> Products
        {
            get
            {
                return products.AsReadOnly();
            }
        }

        private int NextId
        {
            get
            {
                if(!products.Any())
                {
                    return 1;
                }

                return products.Select(p => p.Id).Max() + 1;
            }
        }

        public void Delete(int id)
        {
            if (products == null)
            {
                return;
            }
            var itemToDelete = products.FirstOrDefault(i => i.Id == id);

            if (itemToDelete != null)
            {
                products.Remove(itemToDelete);
            }
        }

        public Product AddOrUpdate(Product p)
        {
            bool isAdd = false;
            if(p.Id == 0)
            {
                isAdd = true;
                p.Id = NextId;
            }

            if(isAdd)
            {
                products.Add(p);
            }

            return p;
        }

        private InventoryServiceProxy()
        {
            //TODO: remove sample data on checkin
            products = new List<Product>()
            {
                new Product{Id = 1,Name = "Apples", Price=1.75M, Quantity=2}
                , new Product{Id = 2,Name = "Juice", Price=10M, Quantity = 4}
                , new Product{Id = 3,Name = "Coffee", Price=137.11M, Quantity =10}
            };
        }

        public static InventoryServiceProxy Current
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new InventoryServiceProxy();
                    }
                }
                return instance;
            }
        }
    }
}
