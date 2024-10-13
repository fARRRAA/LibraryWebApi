﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LibraryWebApi.DataBaseContext;
using LibraryWebApi.Model;
using LibraryWebApi.Requests;
using LibraryWebApi.Interfaces;
using Microsoft.AspNetCore.Authorization;
using LibraryWebApi.Controllers;
using static System.Reflection.Metadata.BlobBuilder;
using System.Linq;
//using LibraryWebApi.Requests;
namespace LibraryWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : Controller
    {

        private readonly IBookService _books;
        private readonly IGenreService _genre;
        public Check Check;

        public BooksController(IBookService bookService, Check check, IGenreService genreService)
        {
            _books = bookService;
            Check = check;
            _genre = genreService;
        }
        [HttpGet]
        [Route("getAllBooks")]
        public async Task<IActionResult> GetAllBooks([FromQuery] string? author, [FromQuery] string? genre, [FromQuery] int? year, [FromQuery] int? page,
        [FromQuery] int? pageSize)
        {
            var books = _books.GetAllBooks(author, genre, year, page, pageSize);
            Task.Delay(5000).Wait();
            return new OkObjectResult(new
            {
                books = books
            });
        }
        [Authorize]
        [HttpPost]
        [Route("addNewBook")]
        public async Task<IActionResult> AddNewBook([FromQuery] CreateBook book)
        {

            if (string.IsNullOrWhiteSpace(book.Title) || string.IsNullOrWhiteSpace(book.Author) || string.IsNullOrWhiteSpace(book.Description) || string.IsNullOrWhiteSpace(Convert.ToString(book.Id_Genre)) || string.IsNullOrWhiteSpace(book.Description) || string.IsNullOrWhiteSpace(Convert.ToString(book.Year)))
            {
                return new OkObjectResult(new
                {
                    error = BadRequest("fill in all fields")
                });
            }

            if (_books.GetAll().Any(b => b.Author == book.Author && b.Title == book.Title))
            {
                return new OkObjectResult(new
                {
                    error = NotFound("this book is already exists")
                });
            }
            return Ok();
        }
        [Authorize]
        [HttpPut]
        [Route("updateBook/{id}")]
        public async Task<IActionResult> UpdateBook(int id, [FromQuery] CreateBook book)
        {
            bool admin = Check.IsUserAdmin();
            if (!admin)
            {
                return new OkObjectResult(new
                {
                    error = Unauthorized("only admin could do this")
                });
            }
            if (!_books.BookExists(id))
            {
                return new OkObjectResult(new
                {
                    error = NotFound("book with that id don`t exists")
                });
            }
            if (string.IsNullOrWhiteSpace(book.Title) || string.IsNullOrWhiteSpace(book.Author) || string.IsNullOrWhiteSpace(book.Description) || string.IsNullOrWhiteSpace(Convert.ToString(book.Id_Genre)) || string.IsNullOrWhiteSpace(book.Description) || string.IsNullOrWhiteSpace(Convert.ToString(book.Year)))
            {
                return new OkObjectResult(new
                {
                    error = BadRequest("fill in all fields")
                });

            }
            await _books.UpdateBook(id, book);
            return Ok();

        }
        [Authorize]
        [HttpDelete]
        [Route("deleteBook/{id}")]
        public async Task<ActionResult> DeleteBook(int id)
        {

            bool admin = Check.IsUserAdmin();
            if (!admin)
            {
                return new OkObjectResult(new
                {
                    error = Unauthorized("only admin could do this")
                });
            }
            if (!_books.BookExists(id))
            {
                return new OkObjectResult(new
                {
                    error = NotFound("book with that id don`t exists")
                });
            }
            await _books.DeleteBook(id);
            return Ok();
        }
        [HttpGet]
        [Route("getBooksByGenre/{id}")]
        public async Task<IActionResult> GetBooksByGenre(int id)
        {
            if (!_genre.GenreExists(id))
            {
                return new OkObjectResult(new
                {
                    error = NotFound("genre with that id don`t exists")
                });
            }

            return new OkObjectResult(new
            {
                genres = _books.GetBooksByGenre(id)
            });
        }
        [HttpGet]
        [Route("getBooksByAuthor/{author}")]
        public async Task<IActionResult> GetBooksByAuthor(string author)
        {


            if (!_books.GetAll().Any(b => b.Author == author))
            {
                return new OkObjectResult(new
                {
                    error = NotFound("not found book with that author")
                });
            }
            var books = _books.GetBooksByAuthor(author);
            return new OkObjectResult(new
            {
                books = books
            });
        }
        [HttpGet]
        [Route("getBooksByName/{name}")]
        public async Task<IActionResult> GetBooksByName(string name)
        {
            ;
            if (!_books.GetAll().Any(b => b.Title == name))
            {
                return new OkObjectResult(new
                {
                    error = NotFound("not found book with that name")
                });
            }
            return new OkObjectResult(new
            {
                books = _books.GetBooksByName(name)
            });
        }
        [HttpGet]
        [Route("getAllExemplars")]
        public async Task<IActionResult> GetALlExemplars()
        {

            return new OkObjectResult(new
            {
                copies = _books.GetAllExemplars()
            });
        }
        [HttpGet]
        [Route("getExemplar/{bookId}")]
        public async Task<IActionResult> GetExemplar(int bookId)
        {

            if (!_books.GetAllExemplars().Any(b => b.Book_Id == bookId))
            {
                return new OkObjectResult(new
                {
                    error = BadRequest("could not find exemplars of this book")
                });
            }
            return new OkObjectResult(new
            {
                book = _books.GetExemplar(bookId)
            });
        }
        [HttpGet]
        [Route("getBookbyId")]
        public async Task<IActionResult> GetBookbyId(int id)
        {
            if (!_books.BookExists(id))
            {
                return new OkObjectResult(new
                {
                    error = NotFound(new { message = $"Книга с ID {id} не найдена." })
                });
            }

            return new OkObjectResult(new
            {
                book = _books.GetBookById(id)
            });

        }
    }
}
