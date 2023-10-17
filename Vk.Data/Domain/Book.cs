using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Vk.Base.Model;

namespace Vk.Data.Domain;

[Table("Books", Schema = "dbo")]

public class Book : BaseModel
{
    public int BookNumber { get; set; }            // KitapId
    public string HeadLine { get; set; }           // Baslik
    public int PageCount { get; set; }             // Sayfa Sayisi
    public string Publisher { get; set; }          // Yayin Evi
    public string ISNB { get; set; }               // Uluslararası Standart Kitap Numarasi
    
    public int AuthorId { get; set; }              // Foreign Key From AuthorTable
    public int CategoryId { get; set; }            // Foreign Key From CategoryTable
    
    public virtual Author Author { get; set; }     // Bir Kitabın bir tane yazari olabilir
    public virtual Category Category { get; set; } // Bir Kitabın bir tane kategorisi olabilir
}

class BookConfiguration : IEntityTypeConfiguration<Book>
{
    public void Configure(EntityTypeBuilder<Book> builder)
    {
        // Base Class Configure
        builder.Property(x => x.InsertDate).IsRequired();
        builder.Property(x => x.UpdateDate).IsRequired(false);
        builder.Property(x => x.IsActive).IsRequired().HasDefaultValue(true); 
        
        // Book Field Property
        builder.Property(x => x.BookNumber).IsRequired(true).HasPrecision(10);
        builder.Property(x => x.HeadLine).IsRequired(true).HasMaxLength(50);
        builder.Property(x => x.PageCount).IsRequired(true);
        builder.Property(x => x.Publisher).IsRequired(false).HasMaxLength(50);
        builder.Property(x => x.ISNB).IsRequired(true).HasMaxLength(13);
        
        // Foreigns Property
        builder.Property(x => x.AuthorId).IsRequired(true);
        builder.Property(x => x.CategoryId).IsRequired(true);
        
        // Index
        builder.HasIndex(x => x.BookNumber).IsUnique(true);
        builder.HasIndex(x => x.AuthorId).IsUnique(false);
    }
}