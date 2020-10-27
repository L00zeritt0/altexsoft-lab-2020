using System;
using System.Collections.Generic;
using System.Text;
using HomeTask4.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HomeTask4.Infrastructure.Data.Config
{
    public class RecipeConfig: IEntityTypeConfiguration<Recipe>
    {
        public void Configure(EntityTypeBuilder<Recipe> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasAlternateKey(x => x.Name);
            builder.Property(x => x.Name).HasMaxLength(50);
            builder.HasOne(x => x.Category).WithMany(x => x.ListOfRecipes).HasForeignKey(x => x.RecipeBookCategoryId);
        }
    }
}
