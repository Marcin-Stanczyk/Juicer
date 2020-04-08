﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Juicer.Core;
using Juicer.Data;
using Juicer.Juicer.Data;
using Juicer.Juicer.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Juicer.Pages.Products
{
    public class DetailsModel : PageModel
    {
        private readonly IJuicerRepository repository;
        private readonly IMapper mapper;

        public Product Product { get; set; }

        public RecipeDto[] Recipes { get; set; }

        [TempData]
        public string Message { get; set; }

        public DetailsModel(IJuicerRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<IActionResult> OnGet(int productId)
        {
            Product = await repository.GetProductAsync(productId);
            var recipes = await repository.GetAllRecipesAsync();
            var recipesWithThisProduct = recipes.Where(r => r.Ingredients.Any(p => p.Product.Name == Product.Name));

            Recipes = mapper.Map<RecipeDto[]>(recipesWithThisProduct);

            if (Product == null)
                return RedirectToPage("./NotFound");

            return Page();
        }
    }
}