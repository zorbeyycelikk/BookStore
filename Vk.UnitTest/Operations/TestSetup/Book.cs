using Vk.Data.Context;
using Vk.Data.Domain;

namespace Vk.UnitTest.TestSetup;

public static class Books
{
    public static void AddBooks(this VkDbContext dbContext)
    {
        dbContext.AddRange(new Book 
            {
                Id = 1,
                InsertDate = DateTime.Now.AddDays(-3),
                UpdateDate = DateTime.Now.AddMinutes(-30),
                IsActive = true,
                BookNumber = 1,
                HeadLine = "Epic Journey of the Phoenix",
                PageCount = 300,
                Publisher = "Adventure Books Publishing",
                ISNB = "1234567890123",
                AuthorId = 6,
                CategoryId = 11
                
            },
            new Book
            {
                Id = 2,
                InsertDate = DateTime.Now.AddDays(-3),
                UpdateDate = DateTime.Now.AddMinutes(-30),
                IsActive = true,
                BookNumber = 1,
                HeadLine = "Whispers in the Shadows 1",
                PageCount = 356,
                Publisher = "Adventure Books Publishing",
                ISNB = "12345678901",
                AuthorId = 7,
                CategoryId = 14
            },

            new Book
            {
                Id = 3,
                InsertDate = DateTime.Now.AddDays(-3),
                UpdateDate = DateTime.Now.AddMinutes(-30),
                IsActive = true,
                BookNumber = 2,
                HeadLine = "Whispers in the Shadows 2",
                PageCount = 289,
                Publisher = "Adventure Books Publishing",
                ISNB = "12345678902",
                AuthorId = 12,
                CategoryId = 3
            },
            new Book
            {
                Id = 4,
                InsertDate = DateTime.Now.AddDays(-3),
                UpdateDate = DateTime.Now.AddMinutes(-30),
                IsActive = true,
                BookNumber = 4,
                HeadLine = "Whispers in the Shadows 4",
                PageCount = 247,
                Publisher = "Adventure Books Publishing",
                ISNB = "12345678904",
                AuthorId = 10,
                CategoryId = 11
            },

            new Book
            {
                Id = 5,
                InsertDate = DateTime.Now.AddDays(-3),
                UpdateDate = DateTime.Now.AddMinutes(-30),
                IsActive = true,
                BookNumber = 5,
                HeadLine = "Whispers in the Shadows 5",
                PageCount = 398,
                Publisher = "Adventure Books Publishing",
                ISNB = "12345678905",
                AuthorId = 3,
                CategoryId = 6
            }, new Book
            {
                Id = 6,
                InsertDate = DateTime.Now.AddDays(-3),
                UpdateDate = DateTime.Now.AddMinutes(-30),
                IsActive = true,
                BookNumber = 6,
                HeadLine = "Whispers in the Shadows 6",
                PageCount = 213,
                Publisher = "Adventure Books Publishing",
                ISNB = "12345678906",
                AuthorId = 14,
                CategoryId = 2
            }, new Book
            {
                Id = 7,
                InsertDate = DateTime.Now.AddDays(-3),
                UpdateDate = DateTime.Now.AddMinutes(-30),
                IsActive = true,
                BookNumber = 7,
                HeadLine = "Whispers in the Shadows 7",
                PageCount = 365,
                Publisher = "Adventure Books Publishing",
                ISNB = "12345678907",
                AuthorId = 8,
                CategoryId = 15
            }, new Book
            {
                Id = 8,
                InsertDate = DateTime.Now.AddDays(-3),
                UpdateDate = DateTime.Now.AddMinutes(-30),
                IsActive = true,
                BookNumber = 8,
                HeadLine = "Whispers in the Shadows 8",
                PageCount = 278,
                Publisher = "Adventure Books Publishing",
                ISNB = "12345678908",
                AuthorId = 2,
                CategoryId = 9
            }, new Book
            {
                Id = 9,
                InsertDate = DateTime.Now.AddDays(-3),
                UpdateDate = DateTime.Now.AddMinutes(-30),
                IsActive = true,
                BookNumber = 9,
                HeadLine = "Whispers in the Shadows 9",
                PageCount = 324,
                Publisher = "Adventure Books Publishing",
                ISNB = "12345678909",
                AuthorId = 11,
                CategoryId = 5
            }, new Book
            {
                Id = 10,
                InsertDate = DateTime.Now.AddDays(-3),
                UpdateDate = DateTime.Now.AddMinutes(-30),
                IsActive = true,
                BookNumber = 10,
                HeadLine = "Whispers in the Shadows 10",
                PageCount = 376,
                Publisher = "Adventure Books Publishing",
                ISNB = "123456789010",
                AuthorId = 6,
                CategoryId = 13
            }


        );
    }
    
    
}