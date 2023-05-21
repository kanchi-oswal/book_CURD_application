
using Book_app.Data.Models;
using Book_app.Data.RequestModel;
using Book_app.Data.ViewModel;

namespace Book_app.Data.Services
{
    public class AuthorsService
    {
        private AppDbContext _dbContext;
        public AuthorsService(AppDbContext dbContext)
        {

            _dbContext = dbContext;

        }
        public void AddAuthor(AuthorVM author)
        {
            var _author = new Author()
            {
              FullName=author.FullName
            };
            _dbContext.Authors.Add(_author);
            _dbContext.SaveChanges();

        }
        public AuthorWithBooksVM GetAuthorsWithBooks(int authorId)
        {
            var _author = _dbContext.Authors.Where(a => a.Id == authorId).Select(a => new AuthorWithBooksVM()
            {
                FullName = a.FullName,
                BookTitles = a.Book_Authors.Select(a => a.Book.Title).ToList()
            }).FirstOrDefault();
            return _author;    
        }
    }
}
