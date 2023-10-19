using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Vk.Base.Model;


namespace Vk.Data.Domain;
[Table("User", Schema = "dbo")]
public class User : BaseModel
{
    public int UserNumber { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Role { get; set; }
    public DateTime LastActivityDate { get; set; }
    public int PasswordRetryCount { get; set; }
}


public class UserConfigruration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.Property(x => x.InsertDate).IsRequired();
        builder.Property(x => x.UpdateDate).IsRequired(false);
        builder.Property(x => x.IsActive).IsRequired().HasDefaultValue(true);

        builder.Property(x => x.UserNumber).IsRequired();
        builder.Property(x => x.Email).IsRequired().HasMaxLength(50);
        builder.Property(x => x.Password).IsRequired().HasMaxLength(50);
        builder.Property(x => x.FirstName).IsRequired().HasMaxLength(50);
        builder.Property(x => x.LastName).IsRequired().HasMaxLength(50);
        builder.Property(x => x.Role).IsRequired().HasMaxLength(10);
        builder.Property(x => x.LastActivityDate).IsRequired();
        builder.Property(x => x.PasswordRetryCount).IsRequired().HasDefaultValue(0);

    }
}