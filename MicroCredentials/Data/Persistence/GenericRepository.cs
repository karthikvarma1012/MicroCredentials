using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace MicroCredentials.Data.Persistence
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private CustomerDbContext _context = null;
        private DbSet<T> table = null;
        public GenericRepository()
        {
            this._context = new CustomerDbContext();
            table = _context.Set<T>();
        }
        public GenericRepository(CustomerDbContext _context)
        {
            this._context = _context;
            table = _context.Set<T>();
        }
        public IEnumerable<T> GetAll()
        {
            return table.ToList();
        }
        public T GetById(object id)
        {
            return table.Find(id);
        }
        public void Insert(T obj)
        {
            table.Add(obj);
            _context.SaveChanges();
        }
        public void Update(T obj)
        {          
            table.Attach(obj);
            _context.Entry(obj).State = EntityState.Modified;
            _context.SaveChanges();
        }
        public void Delete(object id)
        {
            T existing = table.Find(id);
            table.Remove(existing);
            _context.SaveChanges();
        }
        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
