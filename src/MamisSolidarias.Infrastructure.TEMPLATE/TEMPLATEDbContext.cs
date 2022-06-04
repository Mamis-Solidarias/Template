using MamisSolidarias.Infrastructure.TEMPLATE.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace MamisSolidarias.Infrastructure.TEMPLATE;

public class TEMPLATEDbContext: DbContext
{
    public DbSet<User> Users { get; set; }
    public TEMPLATEDbContext(DbContextOptions<TEMPLATEDbContext> options) : base(options)
    {
    }
    
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(
            model =>
            {
                model.HasKey(t => t.Id);
                model.Property(t => t.Id).ValueGeneratedOnAdd();
                model.Property(t => t.Name)
                    .IsRequired()
                    .HasMaxLength(500);

            }
        );



    }
    
}