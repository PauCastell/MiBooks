using MyBooks.Shared.Dtos;

namespace MyBooks.Web.Models
{
    public class BookImportedVM
    {
        public FileBookDto OriginalData { get; set; }

        public BookImportedVM(FileBookDto originalData)
        {
            OriginalData = originalData;
            Title = string.IsNullOrEmpty(originalData.Title) ? "" : originalData.Title;
            Author = string.IsNullOrEmpty(originalData.Author) ? "" : originalData.Author;
            FileName = originalData.FileName;
        }

        public string FileName { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string? ExternalApiTitle { get; set; }
        public int? PublishYear { get; set; }
        public int? PageNumber { get; set; }
        public bool isValid => !string.IsNullOrEmpty(Title) && !string.IsNullOrEmpty(Author);

    }
}
