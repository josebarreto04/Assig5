using Amazon.Library.Models;

namespace eCommerce.API.DataBase
{
    public static class FakeDatabase
    {
        public static int NextProductId
        {
            get
            {
                if (!Products.Any())
                {
                    return 1;
                }

                return Products.Select(p => p.Id).Max() + 1;
            }
        }
        public static List<Product> Products { get; } =  new List<Product>
            {
                new Product{Id = 1,Name = "Apples", Price=1.75M, Quantity=2}
                , new Product{Id = 2,Name = "Juice", Price=10M, Quantity = 4}
                , new Product{Id = 3,Name = "Coffee", Price=137.11M, Quantity =10}
            };
       
    }
}
