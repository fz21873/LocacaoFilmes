using DAL.Abstractions;
using DAL.Data.Context;
using Microsoft.EntityFrameworkCore;
using Model;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.DAO
{
    public class DirectorDAO:RepositoryDAO<Director>
    {

        public DirectorDAO(SqlContext context):base(context)
        {

        }

       
        public async Task<Director> RetornarFilmeDirector(long id)
        {
            IQueryable<Director> query = _context.Directors
                .Where(x => x.Id == id)
                .Include(x => x.Film);

            return await query.FirstOrDefaultAsync();

        }
    }
}
