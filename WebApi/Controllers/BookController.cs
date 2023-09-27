using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]s")]

public class BookController : ControllerBase
{
    // Staticlerin  yasam döngüsü uygulama çalışınca başlar , bitince sonlanır.
    private static List<Book> BookList = new List<Book>()
    {
        
    }; // end- BookList funct

    [HttpGet]
    public List<Book> GetBooks()
    {
        var bookList = BookList.OrderBy(x => x.Id).ToList<Book>();
        return bookList;
    }
    
    [HttpGet("{id}")]
    public Book GetBookById(int id)
    {
        var book = BookList.Where(book => book.Id == id).SingleOrDefault();
        return book;
    }
    
    /*
     [HttpGet]
     public Book GetBookByIdFromQuery([FromQuery]int id)
       {
       var book = BookList.Where(book => book.Id == id).SingleOrDefault();
       return book;
       }
     */
    
    // Post - Create
    [HttpPost]
    public IActionResult AddBooks([FromBody] Book newBook)
    {
        var book = BookList.SingleOrDefault(x => x.Title == newBook.Title);
        if (book is not null)
        {
            return BadRequest();
        }
        BookList.Add(newBook);
        return Ok();
    }
    
    //Put - Update
    [HttpPut("{id}")]
    public IActionResult UpdateBook(int id, [FromBody] Book updatedBook)
    {
        var book = BookList.SingleOrDefault(x => x.Id == id);
        if (book is null)
        {
            return BadRequest();
        }
        book.Title = updatedBook.Title != default ? updatedBook.Title : book.Title;
        book.GenreId = updatedBook.GenreId != default ? updatedBook.GenreId : book.GenreId;
        book.PageCount = updatedBook.PageCount != default ? updatedBook.PageCount : book.PageCount;
        book.PublishDate = updatedBook.PublishDate != default ? updatedBook.PublishDate : book.PublishDate;
        return Ok();
    }
    
    //Delete
    [HttpDelete("{id}")]
    public IActionResult DeleteBook(int id)
    {
        var book = BookList.SingleOrDefault(x => x.Id == id);
        if (book is null)
        {
            return BadRequest();
        }

        BookList.Remove(book);
        return Ok();
        
    }
    

}