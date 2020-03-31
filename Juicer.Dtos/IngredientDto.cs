using Juicer.Juicer.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Juicer.Juicer.Dtos
{
    public class IngredientDto
    {
        public string ProductName { get; set; }

        public float Amount { get; set; }

        public UnitType Unit { get; set; }
    }
}
