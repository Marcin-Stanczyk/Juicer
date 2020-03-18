using Juicer.Core;
using System.Collections.Generic;
using System.Linq;

namespace Juicer.Data
{
    public class InMemoryProductData : IProductData
    {
        readonly List<Product> products;

        public InMemoryProductData()
        {
            products = new List<Product>()
            {
                new Product { Id = 1, Name = "Strawberries", Category = CategoryType.Berries },
                new Product { Id = 2, Name = "Lemon", Category = CategoryType.Citrus },
                new Product { Id = 3, Name = "Mango", Category = CategoryType.Tropical },
                new Product { Id = 4, Name = "Ananas", Category = CategoryType.Tropical }

            };
        }


        public IEnumerable<Product> GetProductsByName(string name = null)
        {

            if (name == null)
                return products;

            return products.Where(p => p.Name.ToLower().StartsWith(name.ToLower()));
        }
    }
}
