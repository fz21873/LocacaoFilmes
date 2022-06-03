using BAL.RuleBusiness.Abstractions;
using DAL.DAO;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Model;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BAL.RuleBusiness
{
    public class FilmRuleBusiness: DefaultRuleBusiness<Film>
    {
        private readonly FilmDAO _filmDAO;
        public FilmRuleBusiness(FilmDAO filmDAO):base(filmDAO)
        {
            _filmDAO = filmDAO;
        }

        public override async Task ValidateAsync(Film entity, ModelStateDictionary modelState)
        {
            if (!modelState.IsValid)
                return;
        }

        public override Task<IList<Film>> FindAll()
        {
            return this.FindIncludes();
        }


        private async Task<IList<Film>> FindIncludes()
        {
            var findinclude = new List<Expression<Func<Film, object>>>();

            findinclude.Add(x => x.Director);
            return await _filmDAO.QueryIncludes(findinclude);
        }


        public override async Task<Film> FindById(params object[] key)
        {
            return await this.FindFilmesIncludes(key);
        }

        public async Task<Film> FindFilmesIncludes(params object[] chave)
        {
            int id = 0;
            foreach (object key in chave)
            {
                id = Convert.ToInt32(key);
            }
            return await _filmDAO.RetornarDirectorFilm(id);

        }
    }
}
