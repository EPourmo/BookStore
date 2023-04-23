using BookStore.Models.Domain;
using BookStore.Repositories.Abstract;

namespace BookStore.Repositories.Implementation
{
    public class PublisherService : IPublisherService
    {
        private readonly DatabaseContext _context;

        public PublisherService(DatabaseContext context)
        {
            _context = context;
        }

        public bool Add(Publisher model)
        {
            try
            {
                _context.Publishers.Add(model);
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
                    _context.Publishers.Remove(data);
                    _context.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public Publisher FindById(int id)
        {
            return _context.Publishers.Find(id);
        }

        public IEnumerable<Publisher> GetAll()
        {
            return _context.Publishers.ToList();
        }

        public bool Update(Publisher model)
        {
            try
            {
                _context.Publishers.Update(model);
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
