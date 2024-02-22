using api_BookStore.Models;
using api_BookStore.Services;
using Microsoft.AspNetCore.Mvc;

namespace api_BookStore.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : Controller
    {
        // Rota async GET
        private readonly BooksService _booksService;
        public BooksController(BooksService booksService)
        {
            _booksService = booksService;
        }

        [HttpGet]
        public async Task<List<Book>> Get() =>
            await _booksService.GetAsync();

        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<Book>> Get(string id)
        {
            var book = await _booksService.GetAsync(id);
            if(book == null)
            {
                return NotFound();
            }
            return Ok(book);
        }

        // POST
        [HttpPost]
        public async Task<IActionResult> Post(Book newBook)
        {
            await _booksService.CreateAsync(newBook);

            return CreatedAtAction(nameof(Get), new { id = newBook.Id }, newBook);
        }

        // PUT
        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, Book updatedBook)
        {
            var book = await _booksService.GetAsync(id);
            if(book is null)
                return NotFound();
            updatedBook.Id = book.Id;
            await _booksService.UpdateAsync(id, updatedBook);
            return NoContent();
        }

        // DELETE
        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            var book = await _booksService.GetAsync(id);
            if (book is null)
                return NotFound();
            await _booksService.RemoveAsync(id);
            return NoContent();
        }
       

    }
}
