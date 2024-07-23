using Amazon.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Library.DTO
{
    public class ProductDTO
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public int Id { get; set; }

        public int? Quantity { get; set; }
        public bool IsBuyOneGetOneFree { get; set; }
        public decimal Discount { get; set; }

        public ProductDTO(Product p)
        {
            Name = p.Name;
            Description = p.Description;    
            Price = p.Price;
            Id = p.Id;
            Quantity = p.Quantity;
            Discount= p.Discount;
            IsBuyOneGetOneFree= p.IsBuyOneGetOneFree;
        }
        public ProductDTO(ProductDTO p)
        {
            Name = p.Name;
            Description = p.Description;
            Price = p.Price;
            Id = p.Id;
            Quantity = p.Quantity;
            Discount = p.Discount;
            IsBuyOneGetOneFree = p.IsBuyOneGetOneFree;
            

        }
        public ProductDTO()
        {

        }
    }
}
