using MySongsApi.Domain.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySongsApi.Repositories.Impl
{
    public class BandsRepository : GenericRepository<Band>
    {
        private IQueryable<Band> _query;

        public BandsRepository(DbContext context) : base(context)
        {
            _query = _context.Set<Band>().Include(band => band.Albums);
        }

        public override IEnumerable<Band> Select()
        {
            return _query.ToList();
        }

        public override Band SelectById(int id)
        {
            return _query.SingleOrDefault(band => band.Id == id);
        }

        public override IEnumerable<Band> SelectWhere(Func<Band, bool> predicate)
        {
            return _query.Where(predicate);
        }
    }
}
