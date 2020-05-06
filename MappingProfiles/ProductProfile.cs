using System;
using AutoMapper;
using Juicer.Core;
using Juicer.Juicer.Dtos;

namespace JuicerApp.MappingProfiles
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            this.CreateMap<Product, ProductDto>()
                .AfterMap((product, productDto) => 
                productDto.Category = Enum.GetName(typeof(CategoryType), product.Category));

            this.CreateMap<ProductDto, Product>()
                .AfterMap((productDto, product) =>
                product.Category = Enum.Parse<CategoryType>(productDto.Category));
        }
    }
}