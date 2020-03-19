using Juicer.Core;
using Juicer.Juicer.Data;
using System.Collections.Generic;
using System.Linq;

namespace Juicer.Data
{
    public class SqlProductData : IProductData
    {
        private readonly JuicerDbContext db;

        public SqlProductData(JuicerDbContext db)
        {
            this.db = db;
        }




        public Product Add(Product newProduct)
        {
            db.Products.Add(newProduct);
            return newProduct;
        }

        public int Commit()
        {
            return db.SaveChanges();
        }

        public Product Delete(int id)
        {
            var product = GetProductById(id);
            if (product != null)
                db.Products.Remove(product);
            return product;
        }

        public Product GetProductById(int id)
        {
            return db.Products.Find(id);
        }

        public IEnumerable<Product> GetProductsByName(string name)
        {
            if (name == null)
                return db.Products;

            var query = db.Products
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
