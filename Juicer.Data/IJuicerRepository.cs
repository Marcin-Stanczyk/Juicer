using Juicer.Core;
using Juicer.Juicer.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Juicer.Juicer.Data
{
    public interface IJuicerRepository
    {
        // General
        void Add<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        Task<bool> SaveChangesAsync();

        // Recipes
        Task<Recipe[]> GetAllRecipesAsync(string name = null);
        Task<Recipe> GetRecipeAsync(int id);

        // Products
        Task<Product[]> GetAllProductsAsync(string name = null);
        Task<Product> GetProductAsync(int id);
    }
}
