using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Projekat.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace Projekat.EfDataAccess.Configuration
{
    public class PostConfiguration : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            builder.Property(x => x.Title)
                .IsRequired();
            builder.HasIndex(x => x.Title)
                .IsUnique();

            builder.Property(x => x.Text)
                .IsRequired();

            builder.HasMany(p => p.CategoryPost)
                .WithOne(cp => cp.Post)
                .HasForeignKey(cp => cp.PostId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(p => p.Rates)
                .WithOne(r => r.Post)
                .HasForeignKey(r => r.PostId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(p => p.Comments)
                .WithOne(c => c.Post)
                .HasForeignKey(c => c.PostId)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
