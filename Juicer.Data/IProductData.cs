using Juicer.Core;
using System.Collections.Generic;

namespace Juicer.Data
{
    public interface IProductData
    {
        IEnumerable<Product> GetProductsByName(string name);
    }
}
