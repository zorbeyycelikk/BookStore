using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Vk.Base.Model;

namespace Vk.Data.Domain;

[Table("Authors", Schema = "dbo")]
public class Author : BaseModel
{
    public int AuthorNumber { get; set; }           // Yazar Numarasi
    public string Name { get; set; }                // Yazar Adi
    public string Surname { get; set; }             // Yazar Soyadi
    public DateTime BirthDate { get; set; }         // Yazar Dogum Tarihi
    
    public virtual List<Book> Books { get; set; }   // Bir yazarın birden fazla kitabı olabilir
}

class AuthorConfiguration : IEntityTypeConfiguration<Author>
{
    public void Configure(EntityTypeBuilder<Author> builder)
    {
        // Base Class Configure
        builder.Property(x => x.InsertDate).IsRequired();
        builder.Property(x => x.UpdateDate).IsRequired(false);
        builder.Property(x => x.IsActive).IsRequired().HasDefaultValue(true); 
        
        // Author Field Property
        builder.Property(x => x.AuthorNumber).IsRequired(true);
        builder.Property(x => x.Name).IsRequired(true).HasMaxLength(20);
        builder.Property(x => x.Surname).IsRequired(true).HasMaxLength(20);
        
        // Index
        builder.HasIndex(x => x.AuthorNumber).IsUnique(true);
        
        // Relation
        builder.HasMany(x => x.Books)
            .WithOne(x => x.Author)
            .HasForeignKey(x => x.AuthorId)
            .IsRequired(true);
    }
}