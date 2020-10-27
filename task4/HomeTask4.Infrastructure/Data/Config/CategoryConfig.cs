using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using HomeTask4.Core.Entities;

namespace HomeTask4.Infrastructure.Data.Config
{
    public class CategoryConfig: IEntityTypeConfiguration<RecipeBookCategory>
    {
        public void Configure(EntityTypeBuilder<RecipeBookCategory> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasOne(x => x.Parent).WithMany(x => x.SubCategories).HasForeignKey(x => x.ParentId);
            builder.Property(x => x.Name).HasMaxLength(50);
            builder.HasData(
                new RecipeBookCategory[]
                { 
                    new RecipeBookCategory{Id = 1, Name = "Category 1", ParentId = null},
                    new RecipeBookCategory{Id = 2, Name = "Category 2", ParentId = null},
                    new RecipeBookCategory{Id = 3, Name = "Category 3", ParentId = null},
                    new RecipeBookCategory{Id = 4, Name = "SubCategory 1/1", ParentId = 1},
                    new RecipeBookCategory{Id = 5, Name = "SubCategory 1/2", ParentId = 1},
                    new RecipeBookCategory{Id = 6, Name = "SubCategory 1/3", ParentId = 1},
                    new RecipeBookCategory{Id = 7, Name = "SubCategory 2/1", ParentId = 2},
                    new RecipeBookCategory{Id = 8, Name = "SubCategory 2/2", ParentId = 2},
                    new RecipeBookCategory{Id = 9, Name = "SubCategory 2/3", ParentId = 2},
                    new RecipeBookCategory{Id = 10, Name = "SubCategory 3/1", ParentId = 3},
                    new RecipeBookCategory{Id = 11, Name = "SubCategory 3/2", ParentId = 3},
                    new RecipeBookCategory{Id = 12, Name = "SubCategory 3/3", ParentId = 3}
                });

        }
    }
}
