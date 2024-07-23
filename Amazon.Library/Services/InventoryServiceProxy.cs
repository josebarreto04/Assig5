﻿using Amazon.Library.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Amazon.Library.Utilities;
using eCommerce.Library.DTO;

namespace Amazon.Library.Services
{
    public class InventoryServiceProxy
    {
        private static InventoryServiceProxy? instance;
        private static object instanceLock = new object();

        private List<ProductDTO> products;

        public ReadOnlyCollection<ProductDTO> Products
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
        public async Task<ProductDTO> Delete(int id)
        {
           var response = await new WebRequestHandler().Delete($"/{id}");
           var itemToDelete =JsonConvert.DeserializeObject<ProductDTO>(response);
           return itemToDelete;
        }
       
        public async Task<IEnumerable<ProductDTO>> Get()
        {
            var result = await new WebRequestHandler().Get("/Inventory");
            var deserializedResult = JsonConvert.DeserializeObject<List<ProductDTO>>(result);
            products = deserializedResult.ToList() ?? new List<ProductDTO>();
            return products;
        }
        public async Task<ProductDTO> AddOrUpdate(ProductDTO p)
        {
            
            var result = await new WebRequestHandler().Post("/Inventory", p);
            return JsonConvert.DeserializeObject<ProductDTO>(result);
        }

        private InventoryServiceProxy()
        {
            var response = new WebRequestHandler().Get("/Inventory").Result;
            products = JsonConvert.DeserializeObject<List<ProductDTO>>(response);
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
        public void ApplyMarkdown(ProductDTO p)
        {
            if (p.Discount > 100 || p.Discount < 0)
            {
                return;
            }
            p.Price = p.Price * (1 - (p.Discount/ 100));

        }

        public async Task<IEnumerable<ProductDTO>> Search(Query? query)
        {
            if (query == null || string.IsNullOrEmpty(query.QueryString))
            {
                return await Get();
            }

            var result = await new WebRequestHandler().Post("/Inventory/Search", query);
            products = JsonConvert.DeserializeObject<List<ProductDTO>>(result) ?? new List<ProductDTO>();
            return Products;
        }
    }
}
