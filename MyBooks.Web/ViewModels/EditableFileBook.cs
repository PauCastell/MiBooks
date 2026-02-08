namespace MyBooks.Web.ViewModels
{
    public class EditableFileBook
    {
        public string FileName { get; set; }
        public string? Title { get; set; }
        public string? Author { get; set; }

        public bool IsSelected { get; set; } = false;
        public bool IsValid => !string.IsNullOrWhiteSpace(Title) && !string.IsNullOrWhiteSpace(Author);
    }
}
