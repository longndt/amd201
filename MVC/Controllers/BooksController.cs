using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVC.Data;
using MVC.Models;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MVC.Controllers
{
    public class BooksController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _env;

        public BooksController(ApplicationDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        // Saves an uploaded image into wwwroot/images and returns the web path to it.
        private async Task<string> SaveImageAsync(IFormFile image)
        {
            var imagesDir = Path.Combine(_env.WebRootPath, "images");
            Directory.CreateDirectory(imagesDir);

            // Use a unique file name so different uploads never overwrite each other.
            var fileName = Guid.NewGuid().ToString("N") + Path.GetExtension(image.FileName);
            var filePath = Path.Combine(imagesDir, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await image.CopyToAsync(stream);
            }

            return "/images/" + fileName;
        }

        // GET: Books
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Book.Include(b => b.Genre);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Books/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Book
                .Include(b => b.Genre)
                .FirstOrDefaultAsync(m => m.BookId == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // GET: Books/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            ViewData["GenreId"] = new SelectList(_context.Genre, "GenreId", "GenreName");
            return View();
        }

        // POST: Books/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BookId,BookTitle,BookPrice,BookImage,GenreId")] Book book, IFormFile Image)
        {
            if (ModelState.IsValid)
            {
                // If a file was uploaded, store it and use its path as the cover.
                // Otherwise keep the URL the user typed into BookImage.
                if (Image != null && Image.Length > 0)
                {
                    book.BookImage = await SaveImageAsync(Image);
                }

                _context.Book.Add(book);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            ViewData["GenreId"] = new SelectList(_context.Genre, "GenreId", "GenreName", book.GenreId);
            return View(book);
        }

        // GET: Books/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Book.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }

            ViewData["GenreId"] = new SelectList(_context.Genre, "GenreId", "GenreName", book.GenreId);
            return View(book);
        }

        // POST: Books/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BookId,BookTitle,BookPrice,BookImage,GenreId")] Book book, IFormFile Image)
        {
            if (id != book.BookId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // A newly uploaded file wins; otherwise keep whatever BookImage
                    // came back from the form (an edited URL or the existing path).
                    if (Image != null && Image.Length > 0)
                    {
                        book.BookImage = await SaveImageAsync(Image);
                    }

                    _context.Update(book);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookExists(book.BookId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["GenreId"] = new SelectList(_context.Genre, "GenreId", "GenreName", book.GenreId);
            return View(book);
        }

        // GET: Books/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Book
                .Include(b => b.Genre)
                .FirstOrDefaultAsync(m => m.BookId == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var book = await _context.Book.FindAsync(id);
            _context.Book.Remove(book);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookExists(int id)
        {
            return _context.Book.Any(e => e.BookId == id);
        }

        [HttpPost]
        public async Task<IActionResult> Search(string keyword)
        {
            var books = await _context.Book.Include(b => b.Genre).Where(b => b.BookTitle.Contains(keyword)).ToListAsync();
            return View("Index",books);
        }

        public async Task<IActionResult> SortPriceASC()
        {
            var books = await _context.Book.Include(b => b.Genre).OrderBy(b => b.BookPrice).ToListAsync();
            return View("Index", books);
        }

        public async Task<IActionResult> SortPriceDESC()
        {
            var books = await _context.Book.Include(b => b.Genre).OrderByDescending(b => b.BookPrice).ToListAsync();
            return View("Index", books);
        }
    }
}
