using Amazon.Library.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace eCommerce.API.DataBase
{
    public class Filebase
    {
        private string _root;
        private static Filebase _instance;


        public static Filebase Current
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new Filebase();
                }

                return _instance;
            }
        }
        public int NextProductId
        {
            get
            {
                if (!Products.Any())
                {
                    return 1;
                }

                return Products.Max(p => p.Id) + 1;
            }
        }
        private Filebase()
        {
            _root = @"C:\Windows\Temp\Products";
        }

        public Product AddOrUpdate(Product p)
        {
            //set up a new Id if one doesn't already exist
            if (p.Id <= 0)
            {
                p.Id = NextProductId;
            }

            //go to the right place]
            string path = $"{_root}\\{p.Id}.json";

            //if the item has been previously persisted
            if (File.Exists(path))
            {
                //blow it up
                File.Delete(path);
            }

            //write the file
            File.WriteAllText(path, JsonConvert.SerializeObject(p));

            //return the item, which now has an id
            return p;
        }

        public List<Product> Products
        {
            get
            {
                var root = new DirectoryInfo(_root);
                var _prods = new List<Product>();
                foreach (var appFile in root.GetFiles())
                {
                    var prod = JsonConvert.DeserializeObject<Product>(File.ReadAllText(appFile.FullName));
                    if (prod != null)
                    {
                        _prods.Add(prod);
                    }
                }
                return _prods;
            }
        }

        public Product Delete(int id)
        {
            string path = $"{_root}\\{id}.json";

            // Check if the file exists
            if (File.Exists(path))
            {
                // Read the product data before deleting
                var product = JsonConvert.DeserializeObject<Product>(File.ReadAllText(path));

                // Delete the file
                File.Delete(path);

                // Return the deleted product
                return product;
            }
            else
            {
                // Handle the case where the product does not exist
                throw new FileNotFoundException($"Product with ID {id} does not exist.");
            }
        }
    }
}

