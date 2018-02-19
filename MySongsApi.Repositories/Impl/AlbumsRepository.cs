using MySongsApi.Domain.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySongsApi.Repositories.Impl
{
    public class AlbumsRepository : GenericRepository<Album>
    {
        private IQueryable<Album> _query;

        public AlbumsRepository(DbContext context) : base(context)
        {
            _query = _context.Set<Album>().Include(album => album.Band).Include(album => album.Songs);
        }

        public override IEnumerable<Album> Select()
        {
            return _query.ToList();
        }

        public override Album SelectById(int id)
        {
            return _query.SingleOrDefault(album => album.Id == id);
        }

        public override IEnumerable<Album> SelectWhere(Func<Album, bool> predicate)
        {
            return Select().Where(predicate);
        }
    }
}
