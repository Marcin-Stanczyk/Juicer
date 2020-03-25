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
            this.CreateMap<Recipe, RecipeDto>();

            this.CreateMap<Ingredient, IngredientDto>();
        }
    }
}
