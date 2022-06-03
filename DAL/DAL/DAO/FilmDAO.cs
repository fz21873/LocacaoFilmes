using DAL.Abstractions;
using DAL.Data.Context;
using Microsoft.EntityFrameworkCore;
using Model;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.DAO
{
    public class FilmDAO: RepositoryDAO<Film>
    {
        public FilmDAO(SqlContext context):base(context)
        {

        }


        public async Task<Film> RetornarDirectorFilm(long id)
        {
            IQueryable<Film> query = _context.Films
                .Where(x => x.Id == id)
                .Include(x => x.Director);

            return await query.FirstOrDefaultAsync();

        }

    }
}
