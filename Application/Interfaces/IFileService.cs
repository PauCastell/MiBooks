using MyBooks.Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyBooks.Application.Interfaces;

public interface IFileService
{
    List<FileBookDto> GetFileBookFromPath(string extension);
    Task<string> RenameFileBookAsync(string path, string bookTitle, string authorName);
    Task MoveFileBook(string originPath, string destinationPath);
}
