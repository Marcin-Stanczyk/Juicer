using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Juicer.Juicer.Dtos
{
    public class RecipeDto
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string Instructions { get; set; }

        public string PhotoPath { get; set; }

        public List<IngredientDto> Ingredients { get; set; }
    }
}
