using Vk.Data.Context;
using Vk.Data.Domain;

namespace Vk.UnitTest.TestSetup;

public static class Categories
{
    public static void AddCategory(this VkDbContext dbContext)
    {
        dbContext.AddRange(new Category
    {
        Id = 1,
        InsertDate = DateTime.Now,
        UpdateDate = null,
        IsActive = true,
        CategoryNumber = 5,
        CategoryName = "Adventure"
    },
    new Category
    {
        Id = 2,
        InsertDate = DateTime.Now.AddDays(-3),
        UpdateDate = DateTime.Now.AddMinutes(-30),
        IsActive = true,
        CategoryNumber = 2,
        CategoryName = "Science Fiction"
    },
    new Category
    {
        Id = 3,
        InsertDate = DateTime.Now.AddDays(-5),
        UpdateDate = DateTime.Now.AddHours(-2),
        IsActive = false,
        CategoryNumber = 10,
        CategoryName = "Mystery"
    },
    new Category
    {
        Id = 4,
        InsertDate = DateTime.Now.AddDays(-1),
        UpdateDate = DateTime.Now.AddSeconds(-10),
        IsActive = true,
        CategoryNumber = 7,
        CategoryName = "Romance"
    },
    new Category
    {
        Id = 5,
        InsertDate = DateTime.Now.AddDays(-7),
        UpdateDate = DateTime.Now.AddMinutes(-45),
        IsActive = true,
        CategoryNumber = 14,
        CategoryName = "Thriller"
    },
    new Category
    {
        Id = 6,
        InsertDate = DateTime.Now.AddDays(-2),
        UpdateDate = DateTime.Now.AddMinutes(-15),
        IsActive = false,
        CategoryNumber = 8,
        CategoryName = "Fantasy"
    },
    new Category
    {
        Id = 7,
        InsertDate = DateTime.Now.AddDays(-6),
        UpdateDate = DateTime.Now.AddHours(-1),
        IsActive = true,
        CategoryNumber = 3,
        CategoryName = "Historical Fiction"
    },
    new Category
    {
        Id = 8,
        InsertDate = DateTime.Now.AddDays(-4),
        UpdateDate = DateTime.Now.AddSeconds(-20),
        IsActive = false,
        CategoryNumber = 12,
        CategoryName = "Biography"
    },
    new Category
    {
        Id = 9,
        InsertDate = DateTime.Now.AddDays(-3),
        UpdateDate = DateTime.Now.AddMinutes(-25),
        IsActive = true,
        CategoryNumber = 6,
        CategoryName = "Science"
    },
    new Category
    {
        Id = 10,
        InsertDate = DateTime.Now.AddDays(-1),
        UpdateDate = DateTime.Now.AddSeconds(-5),
        IsActive = true,
        CategoryNumber = 11,
        CategoryName = "Comedy"
    },
    new Category
    {
        Id = 11,
        InsertDate = DateTime.Now.AddDays(-5),
        UpdateDate = DateTime.Now.AddMinutes(-40),
        IsActive = false,
        CategoryNumber = 13,
        CategoryName = "Horror"
    },
    new Category
    {
        Id = 12,
        InsertDate = DateTime.Now.AddDays(-2),
        UpdateDate = DateTime.Now.AddMinutes(-10),
        IsActive = true,
        CategoryNumber = 9,
        CategoryName = "Drama"
    },
    new Category
    {
        Id = 13,
        InsertDate = DateTime.Now.AddDays(-6),
        UpdateDate = DateTime.Now.AddHours(-1),
        IsActive = false,
        CategoryNumber = 4,
        CategoryName = "Action"
    },
    new Category
    {
        Id = 14,
        InsertDate = DateTime.Now.AddDays(-4),
        UpdateDate = DateTime.Now.AddSeconds(-15),
        IsActive = true,
        CategoryNumber = 15,
        CategoryName = "Poetry"
    },
    new Category
    {
        Id = 15,
        InsertDate = DateTime.Now.AddDays(-3),
        UpdateDate = DateTime.Now.AddMinutes(-20),
        IsActive = true,
        CategoryNumber = 1,
        CategoryName = "Classic"
    }
        );
    }
    
    
}