using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Projekat.Domain;

namespace Projekat.EfDataAccess.Configuration
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.Property(x => x.Name).HasMaxLength(20);
            builder.HasIndex(x => x.Name).IsUnique();
            builder.Property(x => x.Name).IsRequired();

            builder.HasMany(c => c.CategoryPost).WithOne(cp => cp.Category).HasForeignKey(cp => cp.CategoryId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
