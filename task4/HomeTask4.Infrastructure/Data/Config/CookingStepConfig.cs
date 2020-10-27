using System;
using System.Collections.Generic;
using System.Text;
using HomeTask4.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HomeTask4.Infrastructure.Data.Config
{
    public class CookingStepConfig: IEntityTypeConfiguration<CookingStep>
    {
        public void Configure(EntityTypeBuilder<CookingStep> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasOne(x => x.Recipe).WithMany(y => y.Steps).HasForeignKey(f => f.RecipeId);
            builder.Property(x => x.Description).HasMaxLength(50);
        }
    }
}
