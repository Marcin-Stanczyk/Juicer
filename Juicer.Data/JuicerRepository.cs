using Juicer.Core;
using Juicer.Juicer.Core;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Juicer.Juicer.Data
{
    public class JuicerRepository : IJuicerRepository
    {
        private readonly JuicerDbContext context;

        public JuicerRepository(JuicerDbContext context)
        {
            this.context = context;
        }

        public void Add<T>(T entity) where T : class
        {
            context.Add(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            context.Remove(entity);
        }

        public async Task<bool> SaveChangesAsync()
        {
            // Only return success if at least one row was changed.
            return (await context.SaveChangesAsync()) > 0;
        }

        public async Task<Product[]> GetAllProductsAsync(string name = null)
        {
            IQueryable<Product> query = context.Products;

            if (name != null)
                query = query.Where(p => p.Name.StartsWith(name));

            query = query.OrderBy(p => p.Name);

            return await query.ToArrayAsync();
        }

        public async Task<Product> GetProductAsync(int id)
        {
            return await context.Products
                .Where(p => p.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task<Recipe[]> GetAllRecipesAsync(string name = null)
        {
            IQueryable<Recipe> query = context.Recipes
                .Include(r => r.Ingredients)
                .ThenInclude(i => i.Product);

            if (name != null)
                query = query.Where(r => r.Name.StartsWith(name));

            query = query.OrderBy(r => r.Name);

            return await query.ToArrayAsync();
        }

        public async Task<Recipe> GetRecipeAsync(int id)
        {
            return await context.Recipes
                .Include(r => r.Ingredients)
                .ThenInclude(i => i.Product)
                .Where(r => r.Id == id)
                .FirstOrDefaultAsync();
        }
    }
}
