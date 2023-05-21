using Book_app.Data.Services;
using Book_app.Data.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Book_app.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private AuthorsService _authorsService;
        public AuthorsController(AuthorsService authorsService)
        {
            this._authorsService = authorsService;
        }
        [HttpPost("add-author")]
        public IActionResult PostAuthorDetails([FromBody]AuthorVM author)
        {
             _authorsService.AddAuthor(author);
            return Ok();


        }
        [HttpGet("get-author-with-books/{id}")]
        public IActionResult GetAuthorWithBooks(int id)
        {
            var _authorWithBooks = _authorsService.GetAuthorsWithBooks(id);
            return Ok(_authorWithBooks);

        }
    }
}
