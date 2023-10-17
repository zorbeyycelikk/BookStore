// using Microsoft.AspNetCore.Mvc;
//
// namespace WebApi.Controllers;
//
// [ApiController]
// [Route("[controller]s")]
//
// public class BookController : ControllerBase
// {
//     
//     [HttpGet]
//     public IActionResult GetBooks()
//     { 
//         return StatusCode(200);
//         
//     }
//     
//     [HttpGet("{id}")]
//     public IActionResult GetBookById(int id)
//     {
//         return Ok();
//     }
//     
//     // Post - Create
//     [HttpPost]
//     public IActionResult AddBooks([FromBody] Book newBook)
//     {
//         return Ok();
//     }
//     
//     //Put - Update
//     [HttpPut("{id}")]
//     public IActionResult UpdateBook(int id, [FromBody] Book updatedBook)
//     {
//         return Ok();
//     }
//     
//     //Delete
//     [HttpDelete("{id}")]
//     public IActionResult DeleteBook(int id)
//     {
//         return Ok();
//         
//     }
//     
// }