using Application.Interfaces;
using MyBooks.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using MyBooks.Application.Interfaces;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController: ControllerBase
    {
        private readonly IBookService _bookService;
        private readonly IGoogleBooksService _googleBooksService;

        public BooksController(IBookService bookService, IGoogleBooksService googleBookService)
        {
            _bookService = bookService;
            _googleBooksService = googleBookService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBooksAsync()
        {
            var books = await _bookService.GetAllBooksAsync();
             return Ok(books);
        }


        //TODO: Posibilidad de usar un request para más control.
        [HttpGet("google-search")]
        public async Task<IActionResult> SearchGoogleBooks([FromQuery] string title, [FromQuery] string author)
        {
            if(string.IsNullOrEmpty(title) && string.IsNullOrEmpty(author))
            {
                return BadRequest("Se debe proporcionar al menos un título o autor");
            }

            var books = await _googleBooksService.GetBookByTitleAndAuthor(title, author);

            return Ok(books);
        }
    }
}
