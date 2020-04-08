using AutoMapper;
using Juicer.Juicer.Core;
using Juicer.Juicer.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Juicer.MappingProfiles
{
    public class RecipeProfile : Profile
    {
        public RecipeProfile()
        {
            this.CreateMap<Recipe, RecipeDto>()
                .AfterMap((recipe, recipeDto) => 
                recipeDto.Instructions = recipe.Instructions.Split('X', StringSplitOptions.RemoveEmptyEntries));

            this.CreateMap<RecipeDto, Recipe>()
                .AfterMap((recipeDto, recipe) => 
                recipe.Instructions = string.Concat(recipeDto.Instructions));


            this.CreateMap<Ingredient, IngredientDto>()
                .AfterMap((ingredient, ingredientDto) => 
                ingredientDto.Unit = Enum.GetName(typeof(UnitType), ingredient.Unit));

            this.CreateMap<IngredientDto, Ingredient>()
                .ForMember(i => i.Product, o => o.Ignore())
                .AfterMap((ingredientDto, ingredient) =>
                ingredient.Unit = Enum.Parse<UnitType>(ingredientDto.Unit));
        }
    }
}
