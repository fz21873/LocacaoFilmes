using DAL.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DAL.Abstractions
{
    public abstract class RepositoryDAO<T> : IRepositoryDAO<T> where T : class
    {
        public SqlContext _context;
        public RepositoryDAO(SqlContext context)
        {
            _context = context;
        }
        public virtual async Task<IList<T>> FindAll()
        {

            return await _context.Set<T>().ToListAsync();
        }

        public virtual async Task<T> FindById(params object[] key)
        {


            var entidade = await _context.FindAsync<T>(key);

            if (entidade == null)
            {
                return null;
            }

            return entidade;

        }

        public virtual async Task<bool> Create(T model, bool save = true)
        {
            await _context.AddRangeAsync(model);
            if (save)
                await _context.SaveChangesAsync();

            return true;
        }



        public virtual async Task<bool> Update(T model, bool save = true)
        {
            _context.UpdateRange(model);
            if (save)
                await _context.SaveChangesAsync();

            return true;
        }

        public virtual async Task<bool> Delete(T model, bool save = true)
        {
            _context.Remove(model);
            if (save)
                await _context.SaveChangesAsync();

            return true;
        }

        public virtual async Task<IList<T>> QueryIncludes(IList<Expression<Func<T,object>>> Includes) 
        {
             IQueryable<T> query = _context.Set<T>();
             List<T> list = new List<T> { };
            if (Includes != null) 
            {
                foreach (var include in Includes)
                {
                    query = query.Include(include);
                }
            }

            list.AddRange(await query.ToListAsync());

            return list;
        } 

          
    }
}
