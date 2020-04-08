using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Juicer.Core;
using Juicer.Juicer.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Juicer.Pages.Products
{
    public class ListModel : PageModel
    {
        public IEnumerable<Product> Products { get; set; }

        public List<string> Categories = new List<string>();
        private readonly IJuicerRepository repository;

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
            Products = await repository.GetAllProductsAsync(SearchTerm);

            foreach (string category in Enum.GetNames(typeof(CategoryType)))
            {
                if (Products.Any(p => p.Category.ToString() == category))
                    Categories.Add(category);
            }

            return Page();
        }
    }
}