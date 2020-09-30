using System;
using System.Collections.Generic;
using System.Text;
using HomeTask4.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HomeTask4.Infrastructure.Data.Config
{
    public class FoodProductConfig: IEntityTypeConfiguration<FoodProduct>
    {
        public void Configure(EntityTypeBuilder<FoodProduct> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasAlternateKey(x => x.Name);
            builder.Property(x => x.Name).HasMaxLength(50);
            builder.HasData(
                new FoodProduct[]
                {
                    new FoodProduct{Id = 1, Name = "Product 1"},
                    new FoodProduct{Id = 2, Name = "Product 2"},
                    new FoodProduct{Id = 3, Name = "Product 3"},
                    new FoodProduct{Id = 4, Name = "Product 4"},
                    new FoodProduct{Id = 5, Name = "Product 5"},
                    new FoodProduct{Id = 6, Name = "Product 6"}
                });
        }
    }
}
