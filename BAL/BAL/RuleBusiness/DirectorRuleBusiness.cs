using BAL.RuleBusiness.Abstractions;
using DAL.DAO;
using DAL.Data.Context;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BAL.RuleBusiness
{
    public class DirectorRuleBusiness : DefaultRuleBusiness<Director>
    {
        private readonly DirectorDAO _directorDAO;
       
        public DirectorRuleBusiness(DirectorDAO directorDAO) :base(directorDAO)
        {
            _directorDAO = directorDAO;
           
        }
        public override async Task ValidateAsync(Director entity, ModelStateDictionary modelState)
        {
            if (!modelState.IsValid)
                return;
        }


        public override Task<IList<Director>> FindAll()
        {
            

            return this.FindIncludes();
        }

        private async Task<IList<Director>> FindIncludes()
        {
            var findinclude = new List<Expression<Func<Director, object>>>();

            findinclude.Add(x => x.Film);
            return await _directorDAO.QueryIncludes(findinclude);
        }


        public override async Task<Director> FindById(params object[] key)
        {
             return await this.FindFilmesIncludes(key);
        }

        public async Task<Director> FindFilmesIncludes(params object[] chave)
        {
            int id = 0;
            foreach (object key in chave)
            {
                id = Convert.ToInt32(key);
            }
            return await _directorDAO.RetornarFilmeDirector(id);

        }
    }
}
