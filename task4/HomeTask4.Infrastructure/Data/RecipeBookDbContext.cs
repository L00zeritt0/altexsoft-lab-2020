using HomeTask4.Core.Entities;
using HomeTask4.Infrastructure.Data.Config;
using HomeTask4.SharedKernel;
using Microsoft.EntityFrameworkCore;

namespace HomeTask4.Infrastructure.Data
{
    public class RecipeBookDbContext : DbContext
    {
        public DbSet<RecipeBookCategory> RecipeBookCategories { get; set; }
        public DbSet<FoodProduct> FoodProducts { get; set; }
        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<RecipeIngredient> Ingredients { get; set; }
        public DbSet<CookingStep> Steps { get; set; }
        public RecipeBookDbContext(DbContextOptions<RecipeBookDbContext> options)
            : base(options)
        {  
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new CategoryConfig());
            modelBuilder.ApplyConfiguration(new FoodProductConfig());
            modelBuilder.ApplyConfiguration(new RecipeConfig());
            modelBuilder.ApplyConfiguration(new RecipeIngredientConfig());
            modelBuilder.ApplyConfiguration(new CookingStepConfig());
        }
    }
}
