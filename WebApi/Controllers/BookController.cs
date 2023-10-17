using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vk.Data.Context;
using Vk.Data.Domain;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]s")]

public class BookController : ControllerBase
{
    private readonly VkDbContext dbContext;
    public BookController(VkDbContext dbContext)
    {
        this.dbContext = dbContext;
    }
    
    [HttpGet]
    public List<Book> GetBooks()
    { 
        try
        {
            this.HttpContext.Response.StatusCode = 200;
            return dbContext.Set<Book>().AsNoTracking().ToList();
        }
        catch (Exception ex)
        {
            // Hata durumunda bir mesaj fırlat

            this.HttpContext.Response.StatusCode = 500;
            throw new Exception("Veri çekme işlemi sırasında bir hata oluştu.", ex);
        }
    }
    
    [HttpGet("{id}")]
    public Book Get( [FromBody]int id)
    {
        try
        {
            this.HttpContext.Response.StatusCode = 200;
            return dbContext.Set<Book>().Find(id);
        }
        catch (Exception ex)
        {
            // Hata durumunda bir mesaj fırlat

            this.HttpContext.Response.StatusCode = 500;
            throw new Exception("Veri çekme işlemi sırasında bir hata oluştu.", ex);
        }
    }
    
    [HttpPost]
    public void Post([FromBody] Book request)
    {
        try
        {
            dbContext.Set<Book>().Add(request); 
            dbContext.SaveChanges();
        }
        catch (Exception ex)
        {
            // Hata durumunda bir mesaj fırlat
            this.HttpContext.Response.StatusCode = 500;
            throw new Exception("Veri çekme işlemi sırasında bir hata oluştu.", ex);
        }
    }
    
    [HttpPut("{id}")]
    public IActionResult Put(int id, [FromBody] Book request)
    {
        var entity = dbContext.Set<Book>().Find(id);
        if (entity == null)
        {
            return NotFound("Book not found.");
        }
        entity.HeadLine = request.HeadLine;
        dbContext.SaveChanges();
        return Ok();
    }
    
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var entity = dbContext.Set<Book>().Find(id);
        if (entity == null)
        {
            return NotFound("Book not found.");
        }
        entity.IsActive = false;
        entity.UpdateDate = DateTime.UtcNow;
        dbContext.SaveChanges();
        return Ok();
    }

}