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

        public void AddBook(BookRequest book)
        {
            var _book = new Book()
            {
                Title = book.Title,
                Description = book.Description,
                IsRead = book.IsRead,
                DateRead = book.IsRead ? book.DataRead.Value : null,
                Rate = book.IsRead ? book.Rate : null,
                Genre = book.Genre,
                Author = book.Author,
                CoverUrl = book.CoverUrl,
                DateAdded = DateTime.Now
            };
            _dbContext.Books.Add(_book);
            _dbContext.SaveChanges();

        }
        public List<Book> GetBooks()
        {
            return _dbContext.Books.ToList();
            
        }
        public Book GetBookById(int Id)
        {
            return _dbContext.Books.FirstOrDefault(x => x.Id == Id);

        }
        public Book UpdateBookById(int BookId, BookRequest book)
        {
           var _book = _dbContext.Books.FirstOrDefault(x=>x.Id==BookId);
            if(_book != null)
            {
                _book.Title = book.Title;
                _book.Description = book.Description;
                _book.IsRead = book.IsRead;
                _book.DateRead = book.IsRead ? book.DataRead.Value : null;
                _book.Rate = book.IsRead ? book.Rate : null;
                _book.Genre = book.Genre;
                _book.Author = book.Author;
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
            
          

        }
    }
}
