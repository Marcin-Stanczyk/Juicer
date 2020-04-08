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
    public class DeleteModel : PageModel
    {
        private readonly IJuicerRepository repository;

        public Product Product { get; set; }

        public DeleteModel(IJuicerRepository repository)
        {
            this.repository = repository;
        }

        public async Task<IActionResult> OnGet(int productId)
        {
            Product = await repository.GetProductAsync(productId);

            if (Product == null)
                return RedirectToPage("./NotFound");

            return Page();
        }

        public async Task<IActionResult> OnPost(int productId)
        {
            var product = await repository.GetProductAsync(productId);

            if (product == null)
                return RedirectToPage("./NotFound");

            repository.Delete(product);
            await repository.SaveChangesAsync();

            TempData["Message"] = $"{product.Name} deleted.";
            return RedirectToPage("./List");
        }
    }
}