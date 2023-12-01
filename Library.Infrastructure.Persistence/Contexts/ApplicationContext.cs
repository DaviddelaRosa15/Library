using Library.Core.Domain.Common;
using Library.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Infrastructure.Persistence.Contexts
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { }

        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<AuditableBaseEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.Id = Guid.NewGuid().ToString().Substring(5, 8);
                        entry.Entity.Created = DateTime.UtcNow;
                        entry.Entity.CreatedBy = "DefaultLibrayUser";
                        break;
                    case EntityState.Modified:
                        entry.Entity.LastModified = DateTime.UtcNow;
                        entry.Entity.LastModifiedBy = "DefaultLibraryUser";
                        break;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //FLUENT API
            base.OnModelCreating(modelBuilder);
            modelBuilder.UseSerialColumns();

            #region Tables

            modelBuilder.Entity<Author>()
                .ToTable("Authors");
            
            modelBuilder.Entity<Book>()
                .ToTable("Books");

            #endregion

            #region Primary keys

            modelBuilder.Entity<Author>()
                .HasKey(x => x.Id);

            modelBuilder.Entity<Book>()
                .HasKey(x => x.Id);

            #endregion

            #region Relationships

            modelBuilder.Entity<Author>()
                .HasMany(x => x.Books)
                .WithOne(x => x.Author)
                .HasForeignKey(x => x.AuthorId)
                .OnDelete(DeleteBehavior.Cascade);

            #endregion

            #region Property configurations

            modelBuilder.Entity<Book>()
                .Property(x => x.Title)
                .IsRequired(true);

            modelBuilder.Entity<Author>()
                .Property(x => x.Name)
                .IsRequired(true);
            
            #endregion
        }

        public void TruncateTables()
        {
            Books.RemoveRange(Books);
            Authors.RemoveRange(Authors);

            SaveChanges();
        }
    }
}
