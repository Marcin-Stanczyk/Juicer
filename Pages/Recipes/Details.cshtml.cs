using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Juicer.Juicer.Core;
using Juicer.Juicer.Data;
using Juicer.Juicer.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Juicer.Pages.Recipes
{
    public class DetailsModel : PageModel
    {
        private readonly IRecipeData recipeData;
        private readonly IMapper mapper;

        public RecipeDto RecipeDto { get; set; }

        public DetailsModel(IRecipeData recipeData, IMapper mapper)
        {
            this.recipeData = recipeData;
            this.mapper = mapper;
        }

        public IActionResult OnGet(int recipeId)
        {
            RecipeDto = mapper.Map<RecipeDto>(recipeData.GetRecipeById(recipeId));

            if (RecipeDto == null)
                return RedirectToPage("./NotFound");

            return Page();
        }
    }
}