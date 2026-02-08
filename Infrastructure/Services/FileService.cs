using Application.Interfaces;
using MyBooks.Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyBooks.Infrastructure.Services
{
    //TODO: Implementar control de errores y validaciones.
    public class FileService : IFileService
    {
        public List<FileBookDto> GetFileBookFromPath(string path, string extension)
        {
            var files = GetFilesFromPath(path, extension);
            var fileBookDtos = new List<FileBookDto>();
            foreach (var file in files)
            {
                var fileBookDto = MapFilestoFileBookDto(file);
                fileBookDtos.Add(fileBookDto);
            }
            return fileBookDtos;
        }

        private string[] GetFilesFromPath(string path, string extension)
        {
            return Directory.GetFiles(path, extension);
        }

        private FileBookDto MapFilestoFileBookDto(string file)
        {
            var fileWithoutExtension = Path.GetFileNameWithoutExtension(file);
            var fileParts = fileWithoutExtension.Split('-');

            return new FileBookDto
            {
                FileName = Path.GetFileName(file),
                Title = fileParts.Length == 2 ? fileParts[0].Trim() : null,
                Author = fileParts.Length == 2 ? fileParts[1].Trim() : null,
                Path = file
            };
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
