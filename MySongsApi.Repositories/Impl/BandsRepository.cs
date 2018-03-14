using MySongsApi.Domain.Models;
using MySongsApi.Infrastructure.Context;
using MySongsApi.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySongsApi.Repositories.Impl
{
    public class BandsRepository : IBandRepository
    {
        private IQueryable<Band> _query;
        private DbContext _context = new MySongsDbContext();

        public BandsRepository()
        {
            _query = _context.Set<Band>().Include(band => band.Albums);
        }

        public void Delete(Band entity)
        {
            _context.Set<Band>().Attach(entity);
            _context.Entry(entity).State = EntityState.Deleted;
            _context.SaveChanges();
        }

        public void DeleteById(int id)
        {
            Band entity = SelectById(id);
            Delete(entity);
        }

        public void Insert(Band entity)
        {
            _context.Set<Band>().Add(entity);
            _context.SaveChanges();
        }

        public IEnumerable<Band> Select()
        {
            return _query.ToList();
        }

        public Band SelectById(int id)
        {
            return _query.SingleOrDefault(band => band.Id == id);
        }

        public IEnumerable<Band> SelectWhere(Func<Band, bool> predicate)
        {
            return _query.Where(predicate);
        }

        public void Update(Band entity)
        {
            _context.Set<Band>().Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
        }
    }
}
