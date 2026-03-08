using MyBooks.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyBooks.Application.Interfaces
{
    public interface IGoogleBooksUseCase
    {
        Task<List<GoogleBookDto>> GetBookByTitleAndAuthor(string title, string author);
    }
}
