using Juicer.Juicer.Core;
using Juicer.Juicer.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Juicer.JuicerData
{
    public class SqlRecipeData : IRecipeData
    {
        private readonly JuicerDbContext db;

        public SqlRecipeData(JuicerDbContext db)
        {
            this.db = db;
        }


        public Recipe Add(Recipe newRecipe)
        {
            db.Recipes.Add(newRecipe);
            return newRecipe;
        }

        public int Commit()
        {
            return db.SaveChanges();
        }

        public Recipe Delete(int id)
        {
            var recipe = GetRecipeById(id);
            if (recipe != null)
                db.Recipes.Remove(recipe);
            return recipe;
        }

        public Recipe GetRecipeById(int id)
        {
            return db.Recipes.Find(id);
        }

        public IEnumerable<Recipe> GetRecipesByName(string name)
        {
            if (name == null)
                return db.Recipes;

            var query = db.Recipes
                .Where(r => r.Name.StartsWith(name))
                .OrderBy(r => r.Name);
            return query;
        }

        public Recipe Update(Recipe updatedRecipe)
        {
            var recipeInDb = GetRecipeById(updatedRecipe.Id);

            if (recipeInDb != null)
            {
                recipeInDb.Name = updatedRecipe.Name;
                recipeInDb.Description = updatedRecipe.Description;
                recipeInDb.Instructions = updatedRecipe.Instructions;
                recipeInDb.PhotoPath = updatedRecipe.PhotoPath;
                recipeInDb.Ingredients = updatedRecipe.Ingredients;
            }

            return recipeInDb;
        }
    }
}
