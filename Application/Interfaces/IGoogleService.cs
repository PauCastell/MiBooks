
using MyBooks.Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyBooks.Application.Interfaces;

public interface IGoogleBooksService
{
    //TODO: Canviar el retorno de la función a un DTO específico para evitar exponer toda la respuesta de Google Books.
    Task<List<GoogleBookDto>> GetBookByTitleAndAuthor(string title, string author);
    Task<List<GoogleBookDto>> GetBooksByQuery(string query);
}
