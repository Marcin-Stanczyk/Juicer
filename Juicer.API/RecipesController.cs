using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Juicer.Juicer.Core;
using Juicer.Juicer.Data;
using Juicer.Juicer.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Juicer.Juicer.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecipesController : ControllerBase
    {
        private readonly IJuicerRepository repository;
        private readonly IMapper mapper;

        public RecipesController(IJuicerRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<RecipeDto[]>> GetAllRecipes(string name = null)
        {
            var recipes = await repository.GetAllRecipesAsync(name);
            return mapper.Map<RecipeDto[]>(recipes);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<RecipeDto>> GetRecipe(int id)
        {
            var recipe = await repository.GetRecipeAsync(id);

            if (recipe == null)
                return NotFound();

            return mapper.Map<RecipeDto>(recipe);
        }

        [HttpPost]
        public async Task<ActionResult<RecipeDto>> Post([FromBody] RecipeDto recipeDto)
        {
            var existingRecipes = await repository.GetAllRecipesAsync();
            var isNameTaken = existingRecipes.Any(r => r.Name == recipeDto.Name);

            if (isNameTaken)
                return BadRequest("This name of recipe is already taken.");

            var newRecipe = mapper.Map<Recipe>(recipeDto);

            var products = await repository.GetAllProductsAsync();

            for (int i=0; i<recipeDto.Ingredients.Count; i++)
            {
                var currentProductName = recipeDto.Ingredients[i].ProductName;
                var currentProduct = products.Where(p => p.Name == currentProductName).FirstOrDefault();

                if (currentProduct != null)
                    newRecipe.Ingredients[i].Product = currentProduct;
                else
                    return BadRequest($"There is no product with name {currentProductName}.");
            }

            repository.Add(newRecipe);

            if (await repository.SaveChangesAsync())
                return CreatedAtAction("GetRecipe", new { id = newRecipe.Id }, mapper.Map<RecipeDto>(newRecipe));
            else
                return BadRequest("Failed to save the recipe");

        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<RecipeDto>> Update(int id, RecipeDto recipeDto)
        {
            var recipeInDb = await repository.GetRecipeAsync(id);

            if (recipeInDb == null)
                return NotFound($"Could not find recipe with id: {id}.");


            var products = await repository.GetAllProductsAsync();

            for (int i = 0; i < recipeDto.Ingredients.Count; i++)
            {
                var currentProductName = recipeDto.Ingredients[i].ProductName;
                var currentProduct = products.FirstOrDefault(p => p.Name == currentProductName);

                if (currentProduct != null)
                    recipeInDb.Ingredients[i].Product = currentProduct;
                else
                    return BadRequest($"There is no product with name {currentProductName}.");
            }

            mapper.Map(recipeDto, recipeInDb);

            if (await repository.SaveChangesAsync())
                return Ok(mapper.Map<RecipeDto>(recipeInDb));
            else
                return BadRequest("Failed to update the recipe");
        }
    }
}