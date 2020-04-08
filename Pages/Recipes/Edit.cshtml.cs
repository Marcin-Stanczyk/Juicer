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
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Juicer.Pages.Recipes
{
    public class EditModel : PageModel
    {
        private readonly IHtmlHelper htmlHelper;
        private readonly IMapper mapper;
        private readonly IJuicerRepository repository;

        [BindProperty]
        public RecipeDto RecipeDto { get; set; }
        public IEnumerable<SelectListItem> Units { get; set; }
        public IEnumerable<SelectListItem> Products { get; set; }

        public EditModel(IHtmlHelper htmlHelper, IMapper mapper, IJuicerRepository repository)
        {
            this.htmlHelper = htmlHelper;
            this.mapper = mapper;
            this.repository = repository;
        }

        public async Task<IActionResult> OnGet(int? recipeId)
        {
            Units = htmlHelper.GetEnumSelectList<UnitType>();

            var products = await repository.GetAllProductsAsync();
            Products = new SelectList(products.Select(p => p.Name));

            if (recipeId.HasValue)
            {
                var recipe = await repository.GetRecipeAsync(recipeId.Value);
                if (recipe == null)
                    return RedirectToPage("./NotFound");

                RecipeDto = mapper.Map<RecipeDto>(recipe);
            }
            else
                RecipeDto = new RecipeDto();

            return Page();
        }
    }
}