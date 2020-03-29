using Juicer.Core;
using Juicer.Juicer.Data;
using System.Collections.Generic;
using System.Linq;

namespace Juicer.Data
{
    public class SqlProductData : IProductData
    {
        private readonly JuicerDbContext context;

        public SqlProductData(JuicerDbContext context)
        {
            this.context = context;
        }

        public Product Add(Product newProduct)
        {
            context.Products.Add(newProduct);
            return newProduct;
        }

        public int Commit()
        {
            return context.SaveChanges();
        }

        public Product Delete(int id)
        {
            var product = GetProductById(id);
            if (product != null)
                context.Products.Remove(product);
            return product;
        }

        public Product GetProductById(int id)
        {
            return context.Products.Find(id);
        }

        public IEnumerable<Product> GetProductsByName(string name)
        {
            if (name == null)
                return context.Products;

            var query = context.Products
                .Where(p => p.Name.StartsWith(name))
                .OrderBy(p => p.Name);

            return query;
        }

        public Product Update(Product updatedProduct)
        {
            var productInDb = GetProductById(updatedProduct.Id);

            if (productInDb != null)
            {
                productInDb.Name = updatedProduct.Name;
                productInDb.Category = updatedProduct.Category;
            }
            
            return productInDb;
        }
    }
}
