using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Juicer.Core;
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
        public List<Product> Products = new List<Product>();

        public DetailsModel(IMapper mapper, IJuicerRepository repository)
        {
            this.mapper = mapper;
            this.repository = repository;
        }

        public async Task<IActionResult> OnGet(int recipeId)
        {
            var recipe = await repository.GetRecipeAsync(recipeId);
            if (recipe == null)
                return RedirectToPage("./NotFound");

            RecipeDto = mapper.Map<RecipeDto>(recipe);

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