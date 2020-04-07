using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Juicer.Juicer.Data;
using Juicer.Juicer.Dtos;
using Juicer.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Juicer.Pages.Recipes
{
    public class ImageModel : PageModel
    {
        private readonly IJuicerRepository repository;
        private readonly ImageStore imageStore;
        private readonly IMapper mapper;

        public RecipeDto RecipeDto { get; set; }

        public ImageModel(IJuicerRepository repository, ImageStore imageStore, IMapper mapper)
        {
            this.repository = repository;
            this.imageStore = imageStore;
            this.mapper = mapper;
        }

        public async Task<IActionResult> OnGet(int recipeId)
        {
            var recipeInDb = await repository.GetRecipeAsync(recipeId);

            if (recipeInDb == null)
                return RedirectToPage("../Recipes/NotFound");

            RecipeDto = mapper.Map<RecipeDto>(recipeInDb);

            return Page();
        }

        public async Task<IActionResult> OnPost(IFormFile image, int recipeId)
        {
            if (image != null)
            {
                using (var stream = image.OpenReadStream())
                {
                    var imageId = await imageStore.SaveImage(stream);

                    var recipeInDb = await repository.GetRecipeAsync(recipeId);
                    recipeInDb.PhotoPath = imageStore.UriFor(imageId);
                    await repository.SaveChangesAsync();

                    return RedirectToPage("./Details", new { recipeId = recipeInDb.Id });
                }
            }
            return RedirectToPage("../Recipes/NotFound");
        }
    }
}