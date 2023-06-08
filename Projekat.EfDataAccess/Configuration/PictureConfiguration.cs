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
    public class PictureConfiguration : IEntityTypeConfiguration<Picture>
    {
        public void Configure(EntityTypeBuilder<Picture> builder)
        {
            builder.Property(x => x.Src)
                .IsRequired();
            builder.Property(x => x.Alt)
                .IsRequired();

            builder.HasMany(p => p.Posts)
                .WithOne(p => p.Picture)
                .HasForeignKey(p => p.PictureId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
