using Bitirme.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Bitirme.Domain.Data
{
    public class DataContext : IdentityDbContext<Users, Roles, Guid>
    {
        public DataContext()
        {
        }

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Files>()
            .HasOne(f => f.Message)
            .WithMany(m => m.Files)
            .HasForeignKey(f => f.MessageID);

            base.OnModelCreating(builder);
        }

        public DbSet<Messages> Messages { get; set; }
        public DbSet<Groups> Groups { get; set; }
        public DbSet<GroupMessages> GroupMessages { get; set; }
        public DbSet<GroupMembers> GroupMembers { get; set; }
        public DbSet<Files> Files { get; set; }
        public DbSet<Connections> Connections { get; set; }
    }
}
