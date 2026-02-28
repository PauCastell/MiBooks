using MyBooks.Application.Dtos.GoogleBooksDtos;
using MyBooks.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Interfaces
{
    public interface IBookService
    {
        Task<List<VolumeInfo>> GetAllBooksAsync();
    }
}
