using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace RecruitmentAdministrationSystemProject.Services
{
    public interface IDataAccessService<TEntity,in Tpk> where TEntity :class
    {
        Task<List<TEntity>> GetDataAsync();
        Task<TEntity> GetDataAsync(Tpk id);
        Task<bool> UpdateAsync(TEntity entity, Tpk id);
        Task<TEntity> Create(TEntity entity);
        Task<bool> DeleteAsync(Tpk id);
    }
}