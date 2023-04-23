using BookStore.Models.Domain;
using BookStore.Repositories.Abstract;

namespace BookStore.Repositories.Implementation
{
    public class BookService : IBookService
    {
        private readonly DatabaseContext _context;

        public BookService(DatabaseContext context)
        {
            _context = context;
        }

        public bool Add(Book model)
        {
            try
            {
                _context.Books.Add(model);
                _context.SaveChanges();
                return true;

            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Delete(int id)
        {
            try
            {
                var data = this.FindById(id);
                if (data == null)
                {
                    return false;
                }
                else
                {
                    _context.Books.Remove(data);
                    _context.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public Book FindById(int id)
        {
            return _context.Books.Find(id);
        }

        public IEnumerable<Book> GetAll()
        {
            var data = (from book in _context.Books
                        join Publisher in _context.Publishers
                        on book.PublisherId equals Publisher.Id
                        join Author in _context.Authors
                        on book.AuthorId equals Author.Id
                        join genre in _context.Genres
                        on book.GenreId equals genre.Id
                        select new Book
                        {
                            Id = book.Id,
                            Title = book.Title,
                            Isbn = book.Isbn,
                            TotalPage = book.TotalPage,
                            AuthorId = book.AuthorId,
                            PublisherId = book.PublisherId,
                            GenreId = book.GenreId,
                            AuthorName = Author.AuthorName,
                            PublisherName = Publisher.PublisherName,
                            GenreName = genre.Name
                        }).ToList();
            return data;
        }

        public bool Update(Book model)
        {
            try
            {
                _context.Books.Update(model);
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
