using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]s")]

public class BookController : ControllerBase
{
    // Staticlerin  yasam döngüsü uygulama çalışınca başlar , bitince sonlanır.
    private static List<Book> BookList = new List<Book>()
    {
        new Book()
        {
            Id = 1,
            Title = "Lean Startup",
            GenreId = 1, // Personal Growth
            PageCount = 200,
            PublishDate = new DateTime(2001, 06, 12)
        },
        new Book()
        {
            Id = 2,
            Title = "Herland",
            GenreId = 2, // Science Fiction
            PageCount = 250,
            PublishDate = new DateTime(2010, 05, 23)
        },
        new Book()
        {
            Id = 3,
            Title = "Dune",
            GenreId = 2, // Personal Growth
            PageCount = 540,
            PublishDate = new DateTime(2001, 12, 21)
        }
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

}