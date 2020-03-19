using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Juicer.Core;
using Juicer.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Juicer.Pages.Products
{
    public class DeleteModel : PageModel
    {
        private readonly IProductData productData;
        
        public Product Product { get; set; }

        public DeleteModel(IProductData productData)
        {
            this.productData = productData;
        }

        public IActionResult OnGet(int productId)
        {
            Product = productData.GetProductById(productId);

            if (Product == null)
                return RedirectToPage("./NotFound");

            return Page();

        }

        public IActionResult OnPost(int productId)
        {
            var product = productData.Delete(productId);
            productData.Commit();

            if (product == null)
                return RedirectToPage("./NotFound");

            TempData["Message"] = $"{product.Name} deleted.";
            return RedirectToPage("./List");
        }
    }
}