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
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Juicer.Pages.Products
{
    public class EditModel : PageModel
    {
        private readonly IHtmlHelper htmlHelper;
        private readonly IJuicerRepository repository;
        private readonly ImageStore imageStore;
        private readonly IMapper mapper;

        [BindProperty]
        public Product Product { get; set; }
        public IEnumerable<SelectListItem> Categories { get; set; }

        public EditModel(IHtmlHelper htmlHelper, IJuicerRepository repository, ImageStore imageStore, IMapper mapper)
        {
            this.htmlHelper = htmlHelper;
            this.repository = repository;
            this.imageStore = imageStore;
            this.mapper = mapper;
        }

        public async Task<IActionResult> OnGet(int? productId)
        {
            Categories = htmlHelper.GetEnumSelectList<CategoryType>();

            if (productId.HasValue)
            {
                Product = await repository.GetProductAsync(productId.Value);
                if (Product == null)
                    return RedirectToPage("./NotFound");
            } 
            else
                Product = new Product();

            return Page();
        }

        public async Task<IActionResult> OnPost(IFormFile image)
        {
            if (!ModelState.IsValid)
            {
                Categories = htmlHelper.GetEnumSelectList<CategoryType>();
                return Page();
            }

            if (Product.Id > 0)
            {
                var productInDb = await repository.GetProductAsync(Product.Id);
                if (productInDb == null)
                    return RedirectToPage("./NotFound");
           
                mapper.Map(Product, productInDb);
                await repository.SaveChangesAsync();

                if (image != null)
                {
                    using (var stream = image.OpenReadStream())
                    {
                        var imageId = await imageStore.SaveImage(stream);

                        productInDb.PhotoPath = imageStore.UriFor(imageId);
                        await repository.SaveChangesAsync();
                    }
                }
                TempData["Message"] = "Product updated!";
            }
            else
            {
                if (image != null)
                {
                    using (var stream = image.OpenReadStream())
                    {
                        var imageId = await imageStore.SaveImage(stream);

                        Product.PhotoPath = imageStore.UriFor(imageId);
                        await repository.SaveChangesAsync();
                    }
                }
                repository.Add(Product);
                await repository.SaveChangesAsync();
                TempData["Message"] = "Product saved!";
            }
            
            return RedirectToPage("./Details", new { productId = Product.Id});
        }
    }
}