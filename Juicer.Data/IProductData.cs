using Juicer.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Juicer.Data
{
    public interface IProductData
    {
        public IEnumerable<Product> GetAllProducts();
    }

    public class InMemoryProductData : IProductData
    {
        readonly List<Product> products;

        public InMemoryProductData()
        {
            products = new List<Product>()
            {
                new Product { Id = 1, Name = "Jabłko", Category = CategoryType.Ziarnkowe },
                new Product { Id = 2, Name = "Borówki amerykańskie", Category = CategoryType.Jagodowe },
                new Product { Id = 3, Name = "Mango", Category = CategoryType.Tropikalne },
                new Product { Id = 4, Name = "Ananas", Category = CategoryType.Tropikalne }

            };
        }


        public IEnumerable<Product> GetAllProducts()
        {
            return from p in products
                   orderby p.Name
                   select p;
        }
    }
}
