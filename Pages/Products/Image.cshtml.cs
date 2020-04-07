using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Juicer.Core;
using Juicer.Juicer.Data;
using Juicer.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Juicer.Pages.Products
{
    public class ImageModel : PageModel
    {
        private readonly IJuicerRepository repository;
        private readonly ImageStore imageStore;

        public Product Product { get; set; }

        public ImageModel(IJuicerRepository repository, ImageStore imageStore)
        {
            this.repository = repository;
            this.imageStore = imageStore;
        }

        public async Task<IActionResult> OnGet(int productId)
        {
            var productInDb = await repository.GetProductAsync(productId);

            if (productInDb == null)
                return RedirectToPage("./NotFound");

            Product = productInDb;

            return Page();
        }

        public async Task<IActionResult> OnPost(IFormFile image, int productId)
        {
            if (image != null)
            {
                using (var stream = image.OpenReadStream())
                {
                    var imageId = await imageStore.SaveImage(stream);

                    var productInDb = await repository.GetProductAsync(productId);
                    productInDb.PhotoPath = imageStore.UriFor(imageId);
                    await repository.SaveChangesAsync();

                    return RedirectToPage("./Details", new { productId = productInDb.Id });
                }
            }

            return RedirectToPage("./NotFound");
        }
    }
}