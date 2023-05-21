namespace Book_app.Data.ViewModel
{
    public class PublisherVM
    {
        public string Name { get; set; }

    }
    public class PublisherWithBooksAndAuthorsVM
    {
        public string Name { get; set; }
        public List<BookAuthorVm> BookAuthors { get; set; }

    }
    public class BookAuthorVm
    {
        public string BookName { get; set; }
        public List<string> BookAuthor { get; set; }


    }
}
