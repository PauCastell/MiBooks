using MyBooks.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyBooks.Application.Interfaces
{
    public interface IBookRepository
    {
        Task<List<LibroRefactor>> GetAllBooks();
        Task<Autor?> GetAutorByNameAsync(string name);
        Task<LibroRefactor> AddBookAsync(LibroRefactor book);
        Task<Autor> AddAutorAsync(Autor autor);
        Task<LibroRefactor?> GetBookByTitle(string title);
    }
}
