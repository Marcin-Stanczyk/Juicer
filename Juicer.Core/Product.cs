using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Juicer.Core
{

    public class Product
    {
        public int Id { get; set; }

        [Required]
        [StringLength(25)]
        public string Name { get; set; }

        public CategoryType Category { get; set; }

        public string PhotoPath { get; set; }
    }
}
