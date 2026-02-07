using Application.Interfaces;
using MyBooks.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyBooks.Infrastructure.Services
{
    public class FileService : IFileService
    {
        public Task<List<FileBookDto>> GetFileBookFromPathAsync(string path, string extension)
        {
            var files = Directory.GetFiles(path, extension);

        }

        private string[] getFilesFromPath(string path, string extension)
        {
            return Directory.GetFiles(path, extension);
        }



        public Task MoveFileBook(string originPath, string destinationPath)
        {
            throw new NotImplementedException();
        }

        public Task<string> RenameFileBookAsync(string path, string bookTitle, string authorName)
        {
            throw new NotImplementedException();
        }
    }
}
