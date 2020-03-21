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
            throw new NotImplementedException();
        }

        public int Commit()
        {
            throw new NotImplementedException();
        }

        public Recipe Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Recipe GetRecipeById(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Recipe> GetRecipesByName(string name = null)
        {
            if (name == null)
                return recipes;

            return recipes.Where(r => r.Name.ToLower().StartsWith(name.ToLower()));
        }

        public Recipe Update(Recipe updatedRecipe)
        {
            throw new NotImplementedException();
        }
    }
}
