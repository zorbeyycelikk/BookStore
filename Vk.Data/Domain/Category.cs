using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Vk.Base.Model;


namespace Vk.Data.Domain;

[Table("Categories", Schema = "dbo")]

public class Category : BaseModel
{
    public int CategoryNumber { get; set; }           // Kategori Numarasi
    public string CategoryName { get; set; }          // Kategori İsmi
    
    public virtual List<Book> Books { get; set; }     // Bir kategorinin birden fazla kitabı kapsayabilir
}

class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        // Base Class Configure
        builder.Property(x => x.InsertDate).IsRequired();
        builder.Property(x => x.UpdateDate).IsRequired(false);
        builder.Property(x => x.IsActive).IsRequired().HasDefaultValue(true); 
        
        // Category Field Property
        builder.Property(x => x.CategoryNumber).IsRequired(true);
        builder.Property(x => x.CategoryName).IsRequired(true).HasMaxLength(50);
        
        // Index
        builder.HasIndex(x => x.CategoryNumber).IsUnique(true);
    }
}