using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Juicer.Core;
using Juicer.Data;
using Juicer.Juicer.Core;
using Juicer.Juicer.Data;
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

        [BindProperty]
        public Recipe Recipe { get; set; }

        public IEnumerable<SelectListItem> Units { get; set; }

        public IEnumerable<SelectListItem> Products { get; set; }

        public EditModel(IRecipeData recipeData, IHtmlHelper htmlHelper, IProductData productData)
        {
            this.recipeData = recipeData;
            this.htmlHelper = htmlHelper;
            this.productData = productData;
        }

        public IActionResult OnGet(int? recipeId)
        {
            Units = htmlHelper.GetEnumSelectList<UnitType>();

            Products = new SelectList(productData.GetProductsByName(null).Select(p => p.Name));

            if (recipeId.HasValue)
                Recipe = recipeData.GetRecipeById(recipeId.Value);
            else
                Recipe = new Recipe();

            if (Recipe == null)
                return RedirectToPage("./NotFound");

            return Page();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                Units = htmlHelper.GetEnumSelectList<UnitType>();
                return Page();
            }

            if (Recipe.Id > 0)
            {
                recipeData.Update(Recipe);
                TempData["Message"] = $"Recipe {Recipe.Name} updated!";
            }
            else
            {
                recipeData.Add(Recipe);
                TempData["Message"] = "Recipe added!";
            }

            recipeData.Commit();
            return RedirectToPage("./Details", new { recipeId = Recipe.Id });
        }
    }
}