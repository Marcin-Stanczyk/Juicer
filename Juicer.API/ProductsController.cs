using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Juicer.Core;
using Juicer.Juicer.Core;
using Juicer.Juicer.Data;
using Juicer.Juicer.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JuicerApp.Juicer.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IJuicerRepository repository;
        private readonly IMapper mapper;
        public ProductsController(IJuicerRepository repository, IMapper mapper)
        {
            this.mapper = mapper;
            this.repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<ProductDto[]>> GetAllProducts(string name = null)
        {
            var products = await repository.GetAllProductsAsync(name);
            return mapper.Map<ProductDto[]>(products);
        }


        [HttpGet("{id:int}")]
        public async Task<ActionResult<ProductDto>> GetProduct(int id)
        {
            var product = await repository.GetProductAsync(id);

            if (product == null)
                return NotFound();
            else
                return mapper.Map<ProductDto>(product);
        }

        [HttpPost]
        public async Task<ActionResult<ProductDto>> Post([FromBody] ProductDto productDto)
        {
            repository.Add(mapper.Map<Product>(productDto));

            if (await repository.SaveChangesAsync())
                return CreatedAtAction("GetProduct", new { id = productDto.Id }, productDto);
            else
                return BadRequest("Failed to save the product");
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<ProductDto>> Update(int id, [FromBody] ProductDto productDto)
        {
            var productInDb = await repository.GetProductAsync(id);

            if (productInDb == null)
                return NotFound($"Could not find product with id: {id}.");
            
            mapper.Map(productDto, productInDb);

            if (await repository.SaveChangesAsync())
                return Ok(productDto);
            else
                return BadRequest("Failed to update the product.");

        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var productToDelete = await repository.GetProductAsync(id);

            if (productToDelete == null)
                return NotFound($"Could not find product with id: {id}.");

            repository.Delete(productToDelete);

            if (await repository.SaveChangesAsync())
                return Ok();
            else
                return BadRequest("Failed to delete the product.");
        }
    }
}