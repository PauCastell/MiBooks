using Microsoft.Extensions.Logging;
using MyBooks.Application.Interfaces;
using MyBooks.Domain.Common;
using MyBooks.Shared.Dtos;

namespace MyBooks.Application.Services
{
    public class GoogleBooksUseCase : IGoogleBooksUseCase
    {
        private readonly IGoogleBooksService _googleBooksService;
        private readonly ILogger<GoogleBooksUseCase> _logger;
        public GoogleBooksUseCase(IGoogleBooksService googleBookService, ILogger<GoogleBooksUseCase> logger)
        { 
            _googleBooksService = googleBookService;
            _logger = logger;
        }

        public async Task<Result<List<GoogleBookDto>>> GetBookByTitleAndAuthor(string title, string author)
        {
            try
            {
                title = title.Trim();
                author = author.Trim();

                if (string.IsNullOrEmpty(title) && string.IsNullOrEmpty(author))
                {
                    return Result<List<GoogleBookDto>>.Success([]);
                }

                var bookList =  await _googleBooksService.GetBookByTitleAndAuthor(title, author);
                return Result<List<GoogleBookDto>>.Success(bookList);
            }
            catch (Exception ex) 
            {
                _logger.LogError(ex, "Error al buscar libros en la API de Google con título: {Title} y autor: {Author}", title, author);
                return Result<List<GoogleBookDto>>.Failure("Se ha producido un error al buscar los datos en la API de Google");
            } 
        }

        //TODO: Crear Test.
        //TODO: Estructurar logs para que todos tengan el mismo formato.
        public async Task<Result<List<GoogleBookDto>>> GetBookByQuery(string searchText)
        {
            try
            {
                searchText = searchText.Trim();

                if (string.IsNullOrEmpty(searchText)) return Result<List<GoogleBookDto>>.Success([]);

                var bookList = await _googleBooksService.GetBooksByQuery(searchText);

                return Result<List<GoogleBookDto>>.Success(bookList);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al buscar libros en la API de Google con query: {SearchText}", searchText);
                return Result<List<GoogleBookDto>>.Failure("Se ha producido un error al buscar los datos en la API de Google");
            }

        }
    }
}
