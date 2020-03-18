using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Juicer.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Juicer.Pages.Products
{
    public class DetailsModel : PageModel
    {
        public Product Product { get; set; }

        public void OnGet(int productId)
        {
            Product = new Product()
            {
                Id = productId
            };
        }
    }
}