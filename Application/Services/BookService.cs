using MyBooks.Domain.Entities;
using MyBooks.Application.Interfaces;
using Microsoft.Extensions.Logging;
using Application.Dtos;
using MyBooks.Application.Dtos;
using MyBooks.Domain.Common;


namespace Application.Services;

public class BookService : IBookService
{
    private readonly IBookRepository _bookRepository;
    private readonly ILogger<BookService> _logger;

    public BookService(IBookRepository bookRepository, ILogger<BookService> logger)
    {
        _bookRepository = bookRepository;
        _logger = logger;
    }

    //TODO: Test
    public async Task<Result<BookCreatedDto>> AddBookAsync(CreateBookDto book)
    {
        try
        {
            var existingBook = await _bookRepository.GetBookByTitle(book.Title);

            if(existingBook != null) return Result<BookCreatedDto>.Failure($"El libro con el título '{book.Title}' ya existe.");

            var libroAutores = await GetOrCreateAutorAsync(book.Authors);

            var newBook = MapToLibroRefactor(book, libroAutores);

            var insertedBook = await _bookRepository.AddBookAsync(newBook);

            return Result<BookCreatedDto>.Success(new BookCreatedDto
            {
                Id = insertedBook.Id,
                Title = insertedBook.Titulo,
                Author = string.Join(", ", book.Authors)
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al agregar el libro: {Titulo}", book.Title);
            return Result<BookCreatedDto>.Failure($"No ha sido posible guardar el libro");
        }
    }


    private async Task<List<LibroAutor>> GetOrCreateAutorAsync(List<string> authors)
    {
        var libroAutores = new List<LibroAutor>();

        foreach (var autorName in authors)
        {
            var autor = await _bookRepository.GetAutorByNameAsync(autorName);
            if (autor is null)
            {
                autor = await _bookRepository.AddAutorAsync(new Autor { Nombre = autorName });
            }
            libroAutores.Add(new LibroAutor { Autor = autor });
        }
        return libroAutores;
    }

    private LibroRefactor MapToLibroRefactor(CreateBookDto book, List<LibroAutor> libroAutor)
    {
        return new LibroRefactor
        {
            Titulo = book.Title,
            AnoPublicacion = book.PublishYear,
            Paginas = book.PageNumber,
            Descripcion = book.Description,
            LibroAutores = libroAutor,
            FuenteExterna = new FuenteExterna
            {
                TituloExterno = book.ExternalApiTitle,
                SmallImage = book.SmallImage,
                BigImage = book.BigImage
            },
            LibroInput = new LibroInput
            {
                NombreArchivo = book.FileName,
                FechaEntrada = DateTime.UtcNow
            },
        };
    }

    //TODO: Test
    //TODO: Paginar el proceso de petición de libros.
    public async Task<Result<List<BookDto>>> GetAllBooksAsync()
    {
        try
        {
            var books = await _bookRepository.GetAllBooks();
            var librosDto = books.Select(l => new BookDto
            {
                Id = l.Id,
                Titulo = l.Titulo
            });

            return Result<List<BookDto>>.Success(librosDto.ToList());
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "No se ha podido obtener los libros de la bd.");
            return Result<List<BookDto>>.Failure("No se ha podido obtener los libros de la bd.");
        }
    }
}
