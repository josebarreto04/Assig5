﻿using eCommerce.Library.DTO;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Amazon.Library.Models
{
    public class Product
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public int Id { get; set; }

        public int? Quantity { get; set; }
        public bool IsBuyOneGetOneFree { get; set; }
        public decimal Discount { get; set; }
        
      

        public Product() { }
        public Product(Product p)
        {
            Name = p.Name;
            Description = p.Description;
            Price = p.Price;
            Quantity = p.Quantity;
            Id = p.Id;
            Discount = p.Discount;
            IsBuyOneGetOneFree = p.IsBuyOneGetOneFree;
        }
        public Product(ProductDTO d)
        {
            Name = d.Name;
            Description = d.Description;
            Price = d.Price;
            Quantity = d.Quantity;
            Id = d.Id;
            Discount = d.Discount;
            IsBuyOneGetOneFree = d.IsBuyOneGetOneFree;
        }
    }
}
