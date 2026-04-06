using Microsoft.IdentityModel.Tokens;
using MyBooks.Shared.Dtos;

namespace MyBooks.Web.Models;

public class BookImportedVM
{
    public FileBookDto OriginalData { get; set; }

    public BookImportedVM(FileBookDto originalData)
    {
        OriginalData = originalData;
        Title = string.IsNullOrEmpty(originalData.Title) ? "" : originalData.Title;
        Author = string.IsNullOrEmpty(originalData.Author) ? new List<string>() : [originalData.Author];
        FileName = originalData.FileName;
    }

    public string FileName { get; set; }
    public string Title { get; set; }
    public List<string> Author { get; set; }
    public string? ExternalApiTitle { get; set; }
    public int? PublishYear { get; set; }
    public int? PageNumber { get; set; }
    public string? Description { get; set; }
    public string? SmallImage { get; set; }
    public string? BigImage { get; set; }
    public bool isValid => !string.IsNullOrEmpty(Title) && Author.Any();

}
