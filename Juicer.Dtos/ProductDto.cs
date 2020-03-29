using Juicer.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Juicer.Juicer.Dtos
{
    public class ProductDto
    {
        public string Name { get; set; }

        public CategoryType Category { get; set; }

        public string PhotoPath { get; set; }
    }
}
