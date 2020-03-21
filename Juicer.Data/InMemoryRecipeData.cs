using Juicer.Core;
using Juicer.Juicer.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Juicer.Juicer.Data
{
    public class InMemoryRecipeData : IRecipeData
    {
        private readonly List<Recipe> recipes;

        public InMemoryRecipeData()
        {
            recipes = new List<Recipe>()
            {
                new Recipe { 
                    Id = 1, 
                    Name = "Banana Shake", 
                    Description = "Easy, tasty breakfast shake.", 
                    Instructions = "Smash bananas|Add Milk|Mix and serve|",
                    PhotoPath = "~/images/Banana-Shake.jpg",
                    Ingredients = new List<Ingredient>()
                    {
                        new Ingredient
                        {
                            Id = 1,
                            Product = new Product { 
                                Id = 1, 
                                Name = "Banana",
                                Category = CategoryType.Fruits,
                                PhotoPath = "~/images/banana.jpg"
                            },
                            Amount = 2,
                            Unit = UnitType.pcs
                        },
                        new Ingredient
                        {
                            Id = 2,
                            Product = new Product
                            {
                                Id = 2,
                                Name = "Milk",
                                Category = CategoryType.Dairy,
                                PhotoPath = "~/images/milk.jpg"
                            },
                            Amount = 250,
                            Unit = UnitType.ml
                        }
                    }
                }
            };
        }



        public Recipe Add(Recipe newRecipe)
        {
            recipes.Add(newRecipe);
            newRecipe.Id = recipes.Max(r => r.Id) + 1;
            return newRecipe;
        }

        public int Commit()
        {
            return 0;
        }

        public Recipe Delete(int id)
        {
            var recipeToDelete = recipes.FirstOrDefault(r => r.Id == id);

            if (recipeToDelete != null)
                recipes.Remove(recipeToDelete);

            return recipeToDelete;
        }

        public Recipe GetRecipeById(int id)
        {
            return recipes.SingleOrDefault(r => r.Id == id);
        }

        public IEnumerable<Recipe> GetRecipesByName(string name = null)
        {
            if (name == null)
                return recipes;

            return recipes.Where(r => r.Name.ToLower().StartsWith(name.ToLower()));
        }

        public Recipe Update(Recipe updatedRecipe)
        {
            var recipe = recipes.SingleOrDefault(r => r.Id == updatedRecipe.Id);

            if (recipe != null)
            {
                recipe.Name = updatedRecipe.Name;
                recipe.Description = updatedRecipe.Description;
                recipe.Instructions = updatedRecipe.Instructions;
                recipe.Ingredients = updatedRecipe.Ingredients;
                recipe.PhotoPath = updatedRecipe.PhotoPath;
            }

            return updatedRecipe;
        }
    }
}
