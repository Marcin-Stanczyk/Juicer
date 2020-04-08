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
    public class ListModel : PageModel
    {
        private readonly IJuicerRepository repository;

        public IEnumerable<Recipe> Recipes { get; set; }

        [TempData]
        public string Message { get; set; }

        [BindProperty(SupportsGet = true)]
        public string SearchTerm { get; set; }

        public ListModel(IJuicerRepository repository)
        {
            this.repository = repository;
        }

        public async Task<IActionResult> OnGet()
        {
            Recipes = await repository.GetAllRecipesAsync(SearchTerm);

            return Page();
        }
    }
}