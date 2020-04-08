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
        private readonly IJuicerRepository repository;

        public Recipe Recipe { get; set; }

        public DeleteModel(IJuicerRepository repository)
        {
            this.repository = repository;
        }

        public async Task<IActionResult> OnGet(int recipeId)
        {
            Recipe = await repository.GetRecipeAsync(recipeId);

            if (Recipe == null)
                return RedirectToPage("./NotFound");

            return Page();
        }

        public async Task<IActionResult> OnPost(int recipeId)
        {
            var recipe = await repository.GetRecipeAsync(recipeId);
            if (recipe == null)
                return RedirectToPage("./NotFound");

            repository.Delete(recipe);
            await repository.SaveChangesAsync();

            TempData["Message"] = $"{recipe.Name} deleted.";
            return RedirectToPage("./List");
        }
    }
}