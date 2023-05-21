using Book_app.Data.Models;
using Book_app.Data.ViewModel;
using Book_app.Exceptions;
using System.Text.RegularExpressions;

namespace Book_app.Data.Services
{
    public class PublishersService
    {
        private AppDbContext _dbContext;
        public PublishersService(AppDbContext dbContext)
        {

            _dbContext = dbContext;

        }

        public Publisher AddPublisher(PublisherVM publisher)
        {
            if (StringStartsWithNumber(publisher.Name)) throw new PublisherNameException("Name starts with number", publisher.Name);

            var _publisher = new Publisher()
            {
                Name = publisher.Name
            };
            _dbContext.Publishers.Add(_publisher);
            _dbContext.SaveChanges();
            return _publisher;
           
        }
        public Publisher GetPublisherById(int id)
        {
            return _dbContext.Publishers.FirstOrDefault(x => x.Id == id);
        }
        public PublisherWithBooksAndAuthorsVM GetPublisherData(int publisherId)
        {
            var _publisher = _dbContext.Publishers.Where(n => n.Id == publisherId).Select(n => new PublisherWithBooksAndAuthorsVM()
            {
                Name = n.Name,
                BookAuthors = n.Books.Select(n => new BookAuthorVm()
                {
                    BookName = n.Title,
                    BookAuthor = n.Book_Authors.Select(n => n.Author.FullName).ToList(),
                }).ToList()
            }).FirstOrDefault();

            return _publisher;
        }

        public void DeletedPublisherById(int id)
        {
            var _publisher = _dbContext.Publishers.FirstOrDefault(n => n.Id == id);
            if(_publisher != null)
            {
                _dbContext.Publishers.Remove(_publisher); 
                _dbContext.SaveChanges();
            }
            else
            {
                throw new Exception($"The Publisher with id : {id} doesn't exists");
            }
        }

        private bool StringStartsWithNumber(string name)
        {
            return Regex.IsMatch(name, @"^\d");
        }
    }
}
