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
    public class AlbumsRepository : IAlbumRepository
    {
        private IQueryable<Album> _query;
        private DbContext _context = new MySongsDbContext();

        public AlbumsRepository()
        {
            _query = _context.Set<Album>().Include(album => album.Band).Include(album => album.Songs);
        }

        public void Delete(Album entity)
        {
            _context.Set<Album>().Attach(entity);
            _context.Entry(entity).State = EntityState.Deleted;
            _context.SaveChanges();
        }

        public void DeleteById(int id)
        {
            Album entity = SelectById(id);
            Delete(entity);
        }

        public void Insert(Album entity)
        {
            _context.Set<Album>().Add(entity);
            _context.SaveChanges();
        }

        public IEnumerable<Album> Select()
        {
            return _query.ToList();
        }

        public Album SelectById(int id)
        {
            return _query.SingleOrDefault(album => album.Id == id);
        }

        public IEnumerable<Album> SelectWhere(Func<Album, bool> predicate)
        {
            return Select().Where(predicate);
        }

        public void Update(Album entity)
        {
            _context.Set<Album>().Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
        }
    }
}
