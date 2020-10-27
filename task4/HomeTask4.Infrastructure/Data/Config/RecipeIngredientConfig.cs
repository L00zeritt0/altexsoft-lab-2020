using System;
using System.Collections.Generic;
using System.Text;
using HomeTask4.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HomeTask4.Infrastructure.Data.Config
{
    public class RecipeIngredientConfig: IEntityTypeConfiguration<RecipeIngredient>
    {
        public void Configure(EntityTypeBuilder<RecipeIngredient> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasOne(x => x.FoodProduct).WithMany(x => x.RecipeIngredients).HasForeignKey(x => x.FoodProductId);
            builder.HasOne(x => x.Recipe).WithMany(y => y.Ingredients).HasForeignKey(f => f.RecipeId);
            builder.Property(x => x.QuantityOfFoodProduct).HasMaxLength(50);
        }
    }
}
