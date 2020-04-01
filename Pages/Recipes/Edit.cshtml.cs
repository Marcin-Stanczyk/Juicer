using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Juicer.Core;
using Juicer.Data;
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
        private readonly IRecipeData recipeData;
        private readonly IHtmlHelper htmlHelper;
        private readonly IProductData productData;
        private readonly IMapper mapper;

        [BindProperty]
        public RecipeDto RecipeDto { get; set; }

        public IEnumerable<SelectListItem> Units { get; set; }

        public IEnumerable<SelectListItem> Products { get; set; }

        public EditModel(IRecipeData recipeData, IHtmlHelper htmlHelper, IProductData productData, IMapper mapper)
        {
            this.recipeData = recipeData;
            this.htmlHelper = htmlHelper;
            this.productData = productData;
            this.mapper = mapper;
        }

        public IActionResult OnGet(int? recipeId)
        {
            Units = htmlHelper.GetEnumSelectList<UnitType>();

            Products = new SelectList(productData.GetProductsByName(null).Select(p => p.Name));

            if (recipeId.HasValue)
                RecipeDto = mapper.Map<RecipeDto>(recipeData.GetRecipeById(recipeId.Value));
            else
                RecipeDto = new RecipeDto();

            if (RecipeDto == null)
                return RedirectToPage("./NotFound");

            return Page();
        }
    }
}