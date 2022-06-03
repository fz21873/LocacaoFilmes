using DAL.Abstractions;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BAL.RuleBusiness.Abstractions
{
    public abstract class DefaultRuleBusiness<T> : IRuleBusiness<T> where T : class
    {
        protected RepositoryDAO<T> _repository;
        public DefaultRuleBusiness(RepositoryDAO<T> repository)
        {
            _repository = repository;
        }

       
        public virtual Task<IList<T>> FindAll()
        {
            return _repository.FindAll();
        }

        public virtual Task<T> FindById(params object[] key)
        {
            return _repository.FindById(key);
        }

        public virtual Task<bool> Create(T model, bool save = true)
        {
            return _repository.Create(model, save);
        }


        public virtual Task<bool> Update(T model, bool save =true)
        {
            return _repository.Update(model, save);
        }

        public  virtual Task<bool> Delete(T model, bool save =true)
        {
            return _repository.Delete(model, save);
        }



        public abstract Task ValidateAsync(T entity, ModelStateDictionary modelState);
        
    }
}
