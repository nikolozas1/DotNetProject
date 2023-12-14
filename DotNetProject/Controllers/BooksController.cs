using DotNetProject;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Route("api/[controller]")]
[ApiController]
public class BooksCotroller : ControllerBase
{
    private readonly WorldDbContext _context;

    public BooksCotroller(WorldDbContext context)
    {
        _context = context;
    }

    // GET: api/Books
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Book>>> GetBooks()
    {
        if (_context.Books == null)
        {
            return NotFound();
        }

        return await _context.Books.ToListAsync();
    }

    // GET: api/Books/5
    [HttpGet("{title}")]
    public async Task<ActionResult<Book>> GetBook(string title)
    {
        var book = await _context.Books.FirstOrDefaultAsync(p => p.Title == title);

        if (book == null)
        {
            return NotFound();
        }

        return book;
    }

    [HttpPut("{title}")]
    public async Task<IActionResult> PutBook(string title, Book book)
    {
        if (!this.ModelState.IsValid)
            return BadRequest(ModelState);

        if (title != book.Title)
        {
            return BadRequest();
        }

        _context.Update(book);

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!BookExists(title))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }

    [HttpPost]
    public async Task<ActionResult<Book>> PostBook(BookDTO BookDto)
    {
        var mapper = new Mapper();
        var book = mapper.DTOToBook(BookDto);

        if (!this.ModelState.IsValid)
            return BadRequest(ModelState);


        if (_context.Books == null)
        {
            return Problem("Entity set 'BookDbContext.Books'  is null.");
        }
        _context.Books.Add(book);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetBook", new { title = book.Title }, book);
    }

    // DELETE: api/Books/5
    [HttpDelete("{title}")]
    public async Task<IActionResult> DeleteBook(string title)
    {
        if (_context.Books == null)
        {
            return NotFound();
        }
        var book = await _context.Books.FindAsync(title);
        if (book == null)
        {
            return NotFound();
        }

        _context.Books.Remove(book);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool BookExists(string title)
    {
        return (_context.Books?.Any(e => e.Title == title)).GetValueOrDefault();
    }
}