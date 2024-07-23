using Amazon.Library.Models;
using eCommerce.API.DataBase;
using eCommerce.Library.DTO;

namespace eCommerce.API.EC
{
    public class InventoryEC
    {
      
        public InventoryEC()
        {
           
        }

        public async Task< IEnumerable<ProductDTO>> Get()
        {
            return Filebase.Current.Products.Take(100).Select(p => new ProductDTO(p));
        }
        public async Task<ProductDTO> AddorUpdate(ProductDTO p)
        {
            //bool isAdd = false;
            //if (p.Id == 0)
            //{
            //    isAdd = true;
            //    p.Id = FakeDatabase.NextProductId;
            //}

            //if (isAdd)
            //{
            //    FakeDatabase.Products.Add(new Product(p));
            //}
            //else
            //{
            //   var prodToUpdate = FakeDatabase.Products.FirstOrDefault(a => a.Id == p.Id);
            //    if (prodToUpdate != null)
            //    {
                    
            //        var index = FakeDatabase.Products.IndexOf(prodToUpdate);
            //        FakeDatabase.Products.RemoveAt(index);
            //        prodToUpdate = new Product(p);
            //        FakeDatabase.Products.Insert(index,prodToUpdate);
                    
            //    }
            //}

            //return p;
            return new ProductDTO(Filebase.Current.AddOrUpdate(new Product(p)));

        }
        public async Task<ProductDTO> Delete(int id)
        {
            return new ProductDTO(Filebase.Current.Delete(id));

            //var itemtoDelete = FakeDatabase.Products.FirstOrDefault(p => p.Id == id);
            //if (itemtoDelete == null)
            //{
            //    return null;
            //}
            //FakeDatabase.Products.Remove(itemtoDelete);
            //return new ProductDTO(itemtoDelete);
        }

        internal async Task<IEnumerable<ProductDTO>> Search(string query)
        {
            return FakeDatabase.Products.Where(p =>
           (p?.Name != null && p.Name.ToUpper().Contains(query?.ToUpper() ?? string.Empty))
               ||
           (p?.Description != null && p.Description.ToUpper().Contains(query?.ToUpper() ?? string.Empty)))
               .Take(100).Select(p => new ProductDTO(p));

        }
    }
}
