using Book_app.Data;
using Book_app.Data.RequestModel;
using Book_app.Data.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Book_app.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly BooksService _booksService;    


        public BooksController(BooksService booksService)
        {
            
            _booksService = booksService;
        }

        [HttpPost("add-book -with-authors")]
        public IActionResult PostBookDetails([FromBody]BookVM book)
        {
            _booksService.AddBookWithAuthors(book);
          
            return Ok();
        }
        [HttpGet("get-all-books")]
        public IActionResult GetAllBooks()
        {
            var bookList = _booksService.GetBooks();
            return Ok(bookList);
        }
        [HttpGet("get-book-by-Id/{id}")]
        public IActionResult GetBookbyId(int id)
        {
            var book = _booksService.GetBookById(id);

            return Ok(book);
        }
        [HttpPut("update-book-by-Id/{id}")]
        public IActionResult UpdateBookById(int id, [FromBody]BookVM book)
        {
            var updatedBook= _booksService.UpdateBookById(id, book);
            return Ok(updatedBook);

        }
        [HttpDelete("delete-book-by-Id/{id}")]
        public IActionResult DeleteBookById(int id)
        {
            try {
                _booksService.DeleteBookById(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
           
        }

    }
}
