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

        public Product Add(Product newProduct)
        {
            products.Add(newProduct);
            newProduct.Id = products.Max(p => p.Id) + 1;
            return newProduct;
        }

        public int Commit()
        {
            return 0;
        }

        public Product Delete(int id)
        {
            var productToDelete = products.FirstOrDefault(p => p.Id == id);

            if (productToDelete != null)
                products.Remove(productToDelete);

            return productToDelete;
        }

        public Product GetProductById(int id)
        {
            return products.SingleOrDefault(p => p.Id == id);
        }

        public IEnumerable<Product> GetProductsByName(string name = null)
        {

            if (name == null)
                return products;

            return products.Where(p => p.Name.ToLower().StartsWith(name.ToLower()));
        }




        public Product Update(Product updatedProduct)
        {
            var product = products.SingleOrDefault(p => p.Id == updatedProduct.Id);

            // To do: implement AutoMapper
            if (product != null)
            {
                product.Name = updatedProduct.Name;
                product.Category = updatedProduct.Category;
            }
            return product;

        }
    }
}
