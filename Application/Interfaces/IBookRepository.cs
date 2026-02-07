using MyBooks.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyBooks.Application.Interfaces
{
    public interface IBookRepository
    {
        Task<List<LibroRefactor>> GetAllBooks();
    }
}
