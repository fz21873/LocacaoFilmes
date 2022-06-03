using DAL.Abstractions;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Threading.Tasks;

namespace BAL.RuleBusiness.Abstractions
{
    interface IRuleBusiness<T>:IRepositoryDAO<T>
    {
        Task ValidateAsync(T entity, ModelStateDictionary modelState);
    }
}
