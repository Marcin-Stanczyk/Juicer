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


        // To Do: Validate if Category is not CategoryType.None
        [Required]
        public CategoryType Category { get; set; }
    }
}
