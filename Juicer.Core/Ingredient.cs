using Juicer.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Juicer.Juicer.Core
{
    public class Ingredient
    {
        public int Id { get; set; }

        public Product Product { get; set; }

        public float Amount { get; set; }

        public UnitType Unit { get; set; }
    }
}
