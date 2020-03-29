using AutoMapper;
using Juicer.Juicer.Core;
using Juicer.Juicer.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Juicer.JuicerData
{
    public class SqlRecipeData : IRecipeData
    {
        private readonly JuicerDbContext context;

        public SqlRecipeData(JuicerDbContext context)
        {
            this.context = context;
        }


        public Recipe Add(Recipe newRecipe)
        {
            context.Recipes.Add(newRecipe);
            return newRecipe;
        }

        public int Commit()
        {
            return context.SaveChanges();
        }

        public Recipe Delete(int id)
        {
            var recipe = GetRecipeById(id);
            if (recipe != null)
                context.Recipes.Remove(recipe);
            return recipe;
        }

        public Recipe GetRecipeById(int id)
        {
            return context.Recipes
                .Include(r => r.Ingredients)
                .ThenInclude(i => i.Product)
                .SingleOrDefault(r => r.Id == id);
        }

        public IEnumerable<Recipe> GetRecipesByName(string name = null)
        {
            var recipes = context.Recipes
                    .Include(r => r.Ingredients)
                    .ThenInclude(i => i.Product);

            if (name == null)
                return recipes.ToList();

            return recipes
                .Where(r => r.Name.StartsWith(name))
                .OrderBy(r => r.Name)
                .ToList();
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
