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
    public class ListModel : PageModel
    {
        private readonly IProductData productData;

        public IEnumerable<Product> Products { get; set; }

        [BindProperty(SupportsGet = true)]
        public string SearchTerm { get; set; }

        public ListModel(IProductData productData)
        {
            this.productData = productData;
        }

        public void OnGet()
        {
            Products = productData.GetProductsByName(SearchTerm);
        }
    }
}