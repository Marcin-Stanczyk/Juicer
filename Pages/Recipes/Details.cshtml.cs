using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Juicer.Core;
using Juicer.Juicer.Core;
using Juicer.Juicer.Data;
using Juicer.Juicer.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Juicer.Pages.Recipes
{
    public class DetailsModel : PageModel
    {
        private readonly IMapper mapper;
        private readonly IJuicerRepository repository;

        public RecipeDto RecipeDto { get; set; }

        public List<Product> Products { get; set; }

        public DetailsModel(IMapper mapper, IJuicerRepository repository)
        {
            this.mapper = mapper;
            this.repository = repository;
            Products = new List<Product>();
        }

        public async Task<IActionResult> OnGet(int recipeId)
        {
            var recipe = await repository.GetRecipeAsync(recipeId);
            RecipeDto = mapper.Map<RecipeDto>(recipe);

            if (RecipeDto == null)
                return RedirectToPage("./NotFound");

            var products = await repository.GetAllProductsAsync();

            foreach (var ingredient in RecipeDto.Ingredients)
            {
                var product = products.FirstOrDefault(p => p.Name == ingredient.ProductName);
                Products.Add(product);
            }

            return Page();
        }
    }
}