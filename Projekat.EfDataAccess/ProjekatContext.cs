using Microsoft.EntityFrameworkCore;
using Projekat.Domain;
using Projekat.EfDataAccess.Configuration;
using System;

namespace Projekat.EfDataAccess
{
    public class ProjekatContext : DbContext
    {
        public override int SaveChanges()
        {
            foreach (var entry in ChangeTracker.Entries())
            {
                if (entry.Entity is Entity e)
                {
                    switch (entry.State)
                    {
                        case EntityState.Added:
                            e.IsActive = true;
                            e.CreatedAt = DateTime.Now;
                            e.IsDeleted = false;
                            e.ModifiedAt = null;
                            e.DeletedAt = null;
                            break;
                        case EntityState.Modified:
                            e.ModifiedAt = DateTime.Now;
                            break;
                    }
                }
            }
            return base.SaveChanges();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=DESKTOP-GUSPAL9;Initial Catalog=AspBlog_Projekat;Integrated Security=True");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<CategoryPost> CategoryPost { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Picture> Pictures { get; set; }
        public DbSet<UserUseCase> UserUseCases { get; set; }
        public DbSet<UseCaseLog> UseCaseLogs { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Rate> Rates { get; set; }
    }
}
