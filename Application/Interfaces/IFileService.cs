using MyBooks.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Interfaces
{
    public interface IFileService
    {
        Task<List<FileBookDto>> GetFileBookFromPathAsync(string path, string extension);
        Task<string> RenameFileBookAsync(string path, string bookTitle, string authorName);
        Task MoveFileBook(string originPath, string destinationPath);
    }
}
