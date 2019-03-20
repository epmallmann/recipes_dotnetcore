using Microsoft.EntityFrameworkCore;
using Recipes.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Recipes.Data
{
    public class RecipeContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) 
            => optionsBuilder.UseNpgsql("Host=localhost;Database=myrecipes;Username=postgres;Password=postgres");

        public DbSet<Recipe> Recipes { get; set; }

    }
}
