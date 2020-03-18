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
    public class DetailsModel : PageModel
    {
        private readonly IProductData productData;

        public Product Product { get; set; }

        [TempData]
        public string Message { get; set; }

        public DetailsModel(IProductData productData)
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
    }
}