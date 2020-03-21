using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Juicer.Juicer.Core
{
    public class Recipe
    {
        public int Id { get; set; }

        [Required]
        [StringLength(40)]
        public string Name { get; set; }

        public string Description { get; set; }

        public string Instructions { get; set; }

        public string PhotoPath { get; set; }

        public List<Ingredient> Ingredients { get; set; }
    }
}
