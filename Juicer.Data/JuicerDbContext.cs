﻿
using Juicer.Core;
using Juicer.Juicer.Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Juicer.Juicer.Data
{
    public class JuicerDbContext : DbContext
    {
        public JuicerDbContext(DbContextOptions<JuicerDbContext> options)
            : base(options)
        { }

        public DbSet<Product> Products { get; set; }

        public DbSet<Ingredient> Ingredients { get; set; }

        public DbSet<Recipe> Recipes { get; set; }
    }
}
