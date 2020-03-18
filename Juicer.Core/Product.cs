using System;
using System.Collections.Generic;
using System.Text;

namespace Juicer.Core
{

    public class Product
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public CategoryType Category { get; set; }
    }
}
