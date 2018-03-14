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
    public class SongsRepository : ISongRepository
    {
        private IQueryable<Song> _query;
        private DbContext _context = new MySongsDbContext();

        public SongsRepository()
        {
            _query = _context.Set<Song>().Include(song => song.Album);
        }

        public void Delete(Song entity)
        {
            _context.Set<Song>().Attach(entity);
            _context.Entry(entity).State = EntityState.Deleted;
            _context.SaveChanges();
        }

        public void DeleteById(int id)
        {
            Song entity = SelectById(id);
            Delete(entity);
        }

        public void Insert(Song entity)
        {
            _context.Set<Song>().Add(entity);
            _context.SaveChanges();
        }

        public IEnumerable<Song> Select()
        {
            return _query.ToList();
        }

        public Song SelectById(int id)
        {
            return _query.SingleOrDefault(song => song.Id == id);
        }

        public IEnumerable<Song> SelectWhere(Func<Song, bool> predicate)
        {
            return _query.Where(predicate);
        }

        public void Update(Song entity)
        {
            _context.Set<Song>().Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
        }
    }
}
