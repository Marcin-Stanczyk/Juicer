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

namespace Juicer.Pages.Products
{
    public class EditModel : PageModel
    {
        private readonly IProductData productData;
        private readonly IHtmlHelper htmlHelper;

        [BindProperty]
        public Product Product { get; set; }
        public IEnumerable<SelectListItem> Categories { get; set; }

        public EditModel(IProductData productData,
                         IHtmlHelper htmlHelper)
        {
            this.productData = productData;
            this.htmlHelper = htmlHelper;
        }

        public IActionResult OnGet(int? productId)
        {
            Categories = htmlHelper.GetEnumSelectList<CategoryType>();
            
            if (productId.HasValue)
                Product = productData.GetProductById(productId.Value);
            else
                Product = new Product();


            if (Product == null)
                return RedirectToPage("./NotFound");

            return Page();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                Categories = htmlHelper.GetEnumSelectList<CategoryType>();
                return Page();
            }

            if (Product.Id > 0)
            {
                productData.Update(Product);
                TempData["Message"] = "Product updated!";
            }
            else
            {
                productData.Add(Product);
                TempData["Message"] = "Product saved!";
            }
            
            productData.Commit();
            return RedirectToPage("./Details", new { productId = Product.Id});
        }
    }
}