using Book_app.Data.Models;
using Book_app.Data.RequestModel;
using System.Security.Cryptography.X509Certificates;
using System.Threading;


namespace Book_app.Data.Services
{
    public class BooksService 
    {
        private AppDbContext _dbContext;
        public BooksService(AppDbContext dbContext)
        {

            _dbContext = dbContext;

        }

        public void AddBookWithAuthors(BookVM book)
        {
            var _book = new Book()
            {
                Title = book.Title,
                Description = book.Description,
                IsRead = book.IsRead,
                DateRead = book.IsRead ? book.DateRead.Value : null,
                Rate = book.IsRead ? book.Rate : null,
                Genre = book.Genre,
                CoverUrl = book.CoverUrl,
                DateAdded = DateTime.Now,
                PublisherId = book.PublisherId,

            };
            _dbContext.Books.Add(_book);
            _dbContext.SaveChanges();

            foreach(var id in book.AuthorIds)
            {
                var _book_Author = new Book_Author()
                {
                    BookId = _book.Id,
                    AuthorId = id

                };
                _dbContext.Book_Authors.Add(_book_Author);
                _dbContext.SaveChanges();
            }

        }
        public List<Book> GetBooks()
        {
            return _dbContext.Books.ToList();
            
        }
        public BookWithAuthorsVM GetBookById(int Id)
        {

            var _bookwithAuthors = _dbContext.Books.Where(n=>n.Id==Id).Select(book => new BookWithAuthorsVM()
            {

                Title = book.Title,
                Description = book.Description,
                IsRead = book.IsRead,
                DateRead = book.IsRead ? book.DateRead.Value : null,
                Rate = book.IsRead ? book.Rate : null,
                Genre = book.Genre,
                CoverUrl = book.CoverUrl,
                PublisherName = book.Publisher.Name,
                AuthorNames = book.Book_Authors.Select(n => n.Author.FullName).ToList()

            }).FirstOrDefault();
            return _bookwithAuthors;


        }
        public Book UpdateBookById(int BookId, BookVM book)
        {
           var _book = _dbContext.Books.FirstOrDefault(x=>x.Id==BookId);
            if(_book != null)
            {
                _book.Title = book.Title;
                _book.Description = book.Description;
                _book.IsRead = book.IsRead;
                _book.DateRead = book.IsRead ? book.DateRead.Value : null;
                _book.Rate = book.IsRead ? book.Rate : null;
                _book.Genre = book.Genre;
                _book.CoverUrl = book.CoverUrl;

                _dbContext.SaveChanges();
            }
            return _book;
        }
        public void DeleteBookById(int bookId)
        {
            var _book = _dbContext.Books.FirstOrDefault(_x => _x.Id == bookId);
            if(_book != null) 
            {
                _dbContext.Books.Remove(_book);
                _dbContext.SaveChanges();
            }
            else
            {
                throw new Exception($"Book with id : {bookId} doesn't exists");
            }
            
          

        }
    }
}
