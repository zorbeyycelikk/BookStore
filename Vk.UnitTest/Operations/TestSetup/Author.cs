using Vk.Data.Context;
using Vk.Data.Domain;

namespace Vk.UnitTest.TestSetup;

public static class Authors
{
    public static void AddAuthor(this VkDbContext dbContext)
    {
        dbContext.AddRange(
            new Author
            {
                Id = 1,
                InsertDate = DateTime.Now,
                UpdateDate = null,
                IsActive = true,
                AuthorNumber = 5,
                Name = "John",
                Surname = "Doe",
                BirthDate = new DateTime(1980, 5, 15)
            },
            new Author
            {
                Id = 2,
                InsertDate = DateTime.Now.AddDays(-3),
                UpdateDate = DateTime.Now.AddMinutes(-30),
                IsActive = true,
                AuthorNumber = 2,
                Name = "Jane",
                Surname = "Smith",
                BirthDate = new DateTime(1992, 8, 20)
            },
            new Author
            {
                Id = 3,
                InsertDate = DateTime.Now.AddDays(-5),
                UpdateDate = DateTime.Now.AddHours(-2),
                IsActive = false,
                AuthorNumber = 10,
                Name = "Michael",
                Surname = "Johnson",
                BirthDate = new DateTime(1975, 3, 10)
            },
            new Author
            {
                Id = 4,
                InsertDate = DateTime.Now.AddDays(-1),
                UpdateDate = DateTime.Now.AddSeconds(-10),
                IsActive = true,
                AuthorNumber = 7,
                Name = "Emily",
                Surname = "Williams",
                BirthDate = new DateTime(1988, 11, 5)
            },
            new Author
            {
                Id = 5,
                InsertDate = DateTime.Now.AddDays(-7),
                UpdateDate = DateTime.Now.AddMinutes(-45),
                IsActive = true,
                AuthorNumber = 14,
                Name = "David",
                Surname = "Brown",
                BirthDate = new DateTime(1972, 6, 25)
            },
            new Author
            {
                Id = 6,
                InsertDate = DateTime.Now.AddDays(-2),
                UpdateDate = DateTime.Now.AddMinutes(-15),
                IsActive = false,
                AuthorNumber = 8,
                Name = "Jessica",
                Surname = "Taylor",
                BirthDate = new DateTime(1985, 9, 12)
            },
            new Author
            {
                Id = 7,
                InsertDate = DateTime.Now.AddDays(-6),
                UpdateDate = DateTime.Now.AddHours(-1),
                IsActive = true,
                AuthorNumber = 3,
                Name = "Daniel",
                Surname = "Miller",
                BirthDate = new DateTime(1990, 2, 18)
            },
            new Author
            {
                Id = 8,
                InsertDate = DateTime.Now.AddDays(-4),
                UpdateDate = DateTime.Now.AddSeconds(-20),
                IsActive = false,
                AuthorNumber = 12,
                Name = "Olivia",
                Surname = "Clark",
                BirthDate = new DateTime(1983, 7, 8)
            },
            new Author
            {
                Id = 9,
                InsertDate = DateTime.Now.AddDays(-3),
                UpdateDate = DateTime.Now.AddMinutes(-25),
                IsActive = true,
                AuthorNumber = 6,
                Name = "William",
                Surname = "Moore",
                BirthDate = new DateTime(1978, 4, 30)
            },
            new Author
            {
                Id = 10,
                InsertDate = DateTime.Now.AddDays(-1),
                UpdateDate = DateTime.Now.AddSeconds(-5),
                IsActive = true,
                AuthorNumber = 11,
                Name = "Sophia",
                Surname = "Anderson",
                BirthDate = new DateTime(1989, 10, 15)
            },
            new Author
            {
                Id = 11,
                InsertDate = DateTime.Now.AddDays(-5),
                UpdateDate = DateTime.Now.AddMinutes(-40),
                IsActive = false,
                AuthorNumber = 13,
                Name = "Christopher",
                Surname = "Wilson",
                BirthDate = new DateTime(1970, 12, 3)
            },
            new Author
            {
                Id = 12,
                InsertDate = DateTime.Now.AddDays(-2),
                UpdateDate = DateTime.Now.AddMinutes(-10),
                IsActive = true,
                AuthorNumber = 9,
                Name = "Ava",
                Surname = "Turner",
                BirthDate = new DateTime(1982, 1, 22)
            },
            new Author
            {
                Id = 13,
                InsertDate = DateTime.Now.AddDays(-6),
                UpdateDate = DateTime.Now.AddHours(-1),
                IsActive = false,
                AuthorNumber = 4,
                Name = "Ethan",
                Surname = "Hill",
                BirthDate = new DateTime(1995, 7, 7)
            },
            new Author
            {
                Id = 14,
                InsertDate = DateTime.Now.AddDays(-4),
                UpdateDate = DateTime.Now.AddSeconds(-15),
                IsActive = true,
                AuthorNumber = 15,
                Name = "Grace",
                Surname = "Baker",
                BirthDate = new DateTime(1976, 9, 18)
            },
            new Author
            {
                Id = 15,
                InsertDate = DateTime.Now.AddDays(-3),
                UpdateDate = DateTime.Now.AddMinutes(-20),
                IsActive = true,
                AuthorNumber = 1,
                Name = "Liam",
                Surname = "Rossi",
                BirthDate = new DateTime(1998, 4, 5)
            }

        );
    }
    
    
}