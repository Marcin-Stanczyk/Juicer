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
using Microsoft.Extensions.Logging;

namespace Juicer.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IMapper mapper;
        private readonly IJuicerRepository repository;

        public RecipeDto[] Recipes { get; set; }

        public IndexModel(ILogger<IndexModel> logger, IMapper mapper, IJuicerRepository repository)
        {
            _logger = logger;
            this.mapper = mapper;
            this.repository = repository;
        }

        public async Task<IActionResult> OnGet()
        {
            
            var recipes = await repository.GetAllRecipesAsync();

            if (recipes.Length > 3)
            {
                var lastThreeRecipes = new Recipe[] {
                    recipes[recipes.Length - 1],
                    recipes[recipes.Length - 2],
                    recipes[recipes.Length - 3]
                };
                Recipes = mapper.Map<RecipeDto[]>(lastThreeRecipes);
            }
            else
            {
                Recipes = mapper.Map<RecipeDto[]>(recipes);
            }
            

            return Page();
        }
    }
}
