using APIPractice.Data;
using APIPractice.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace APIPractice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        //static private List<Book> books = new List<Book>
        //{
        //    new Book { ID = 1, Title = "The Silent Horizon", Author = "Ava Reynolds", YearPublished = 2018 },
        //    new Book { ID = 2, Title = "Shadows of the Mind", Author = "Liam Carter", YearPublished = 2020 },
        //    new Book { ID = 3, Title = "Echoes of Tomorrow", Author = "Maya Thompson", YearPublished = 2015 },
        //    new Book { ID = 4, Title = "The Last Ember", Author = "Noah Bennett", YearPublished = 2022 },
        //    new Book { ID = 5, Title = "Fragments of Light", Author = "Sophia Martinez", YearPublished = 2011 }
        //};

        private readonly APIContext _context;

        public BooksController(APIContext context)
        {
            _context = context;
        }


        [HttpGet]
        public async Task<ActionResult<List<Book>>> GetBooks()
        {
            return Ok(await _context.Books.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Book>> GetBookById(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            return Ok(book);
        }

        [HttpPost]
        public async Task <ActionResult<Book>> AddBook(Book newBook)
        {
            if (newBook == null)
            {
                return BadRequest();
            }
            _context.Books.Add(newBook);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetBookById), new { id = newBook.ID }, newBook);

        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateBook(int id, Book updateBook)
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }

            book.ID = updateBook.ID;
            book.Title = updateBook.Title;
            book.Author = updateBook.Author;
            book.YearPublished = updateBook.YearPublished;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteBook(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }

            _context.Books.Remove(book);
            await _context.SaveChangesAsync();

            return NoContent();
        }

    }
}
