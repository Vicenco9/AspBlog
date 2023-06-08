using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Projekat.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography.X509Certificates;

namespace Projekat.EfDataAccess.Configuration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)

        {
            builder.Property(x => x.FirstName)
                .IsRequired();

            builder.Property(x => x.LastName)
               .IsRequired();

            builder.HasIndex(x => x.Email)
               .IsUnique();
            builder.Property(x => x.Email)
              .IsRequired();

            builder.HasIndex(x => x.Password)
             .IsUnique();
            builder.Property(x => x.Password)
              .IsRequired();

            builder.HasIndex(x => x.Username)
             .IsUnique();
            builder.Property(x => x.Username)
              .IsRequired();

            builder.HasMany(u => u.Posts)
                .WithOne(p => p.User)
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(u => u.Comments)
                .WithOne(c => c.User)
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(u => u.Rates)
                .WithOne(r => r.User)
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(u => u.UserUseCases)
                .WithOne(uc => uc.User)
                .HasForeignKey(uc => uc.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
