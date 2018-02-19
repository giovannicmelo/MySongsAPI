using MySongsApi.Domain.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySongsApi.Repositories.Impl
{
    public class SongsRepository : GenericRepository<Song>
    {
        private IQueryable<Song> _query;

        public SongsRepository(DbContext context) : base(context)
        {
            _query = _context.Set<Song>().Include(song => song.Album);
        }

        public override IEnumerable<Song> Select()
        {
            return _query.ToList();
        }

        public override Song SelectById(int id)
        {
            return _query.SingleOrDefault(song => song.Id == id);
        }

        public override IEnumerable<Song> SelectWhere(Func<Song, bool> predicate)
        {
            return _query.Where(predicate);
        }
    }
}
