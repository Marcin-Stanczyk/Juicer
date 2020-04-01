using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Juicer.Juicer.Core;
using Juicer.Juicer.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Juicer.Pages.Recipes
{
    public class DeleteModel : PageModel
    {
        private readonly IRecipeData recipeData;

        public Recipe Recipe { get; set; }

        public DeleteModel(IRecipeData recipeData)
        {
            this.recipeData = recipeData;
        }

        public IActionResult OnGet(int recipeId)
        {
            Recipe = recipeData.GetRecipeById(recipeId);

            if (Recipe == null)
                return RedirectToPage("./NotFound");

            return Page();
        }

        public IActionResult OnPost(int recipeId)
        {
            var recipe = recipeData.Delete(recipeId);
            recipeData.Commit();

            if (recipe == null)
                return RedirectToPage("./NotFound");

            TempData["Message"] = $"{recipe.Name} deleted.";
            return RedirectToPage("./List");
        }
    }
}