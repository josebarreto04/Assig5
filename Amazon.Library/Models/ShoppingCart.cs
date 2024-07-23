using eCommerce.Library.DTO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amazon.Library.Models
{
    public class ShoppingCart
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ObservableCollection<ProductDTO> Contents { get; set; }
        public ObservableCollection<ProductDTO> Contents2 { get; set; }
        public ShoppingCart()
        {
            Contents = new ObservableCollection<ProductDTO>();
            Contents2 = new ObservableCollection<ProductDTO>();
        }
        
    
    }
}
