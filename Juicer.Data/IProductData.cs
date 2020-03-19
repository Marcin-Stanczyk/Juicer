using Juicer.Core;
using System.Collections.Generic;

namespace Juicer.Data
{
    public interface IProductData
    {
        IEnumerable<Product> GetProductsByName(string name);

        Product GetProductById(int id);

        Product Update(Product updatedProduct);

        Product Add(Product newProduct);

        Product Delete(int id);

        int Commit();
    }
}
