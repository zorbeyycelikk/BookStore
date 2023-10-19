using Microsoft.EntityFrameworkCore;
using Vk.Data.Domain;

namespace Vk.Data.Context;

public class VkDbContext : DbContext
{
    public VkDbContext(DbContextOptions<VkDbContext> options) : base(options)
    {

    }

    // Yapılan database konfigürasyonlarını aktive ettik.
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new AuthorConfiguration());
        modelBuilder.ApplyConfiguration(new BookConfiguration());
        modelBuilder.ApplyConfiguration(new CategoryConfiguration());
        modelBuilder.ApplyConfiguration(new UserConfigruration());
        base.OnModelCreating(modelBuilder);
    }
}