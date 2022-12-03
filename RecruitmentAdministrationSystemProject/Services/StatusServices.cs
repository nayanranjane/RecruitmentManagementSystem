using RecruitmentAdministrationSystemProject.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace RecruitmentAdministrationSystemProject.Services
{
    public class StatusServices : IDataAccessService<Status, int>
    {
        RecruitmentManagementSystemEntities dbAccess;
        public StatusServices(RecruitmentManagementSystemEntities dbAccess)
        {
            this.dbAccess = dbAccess;
        }

        Task<bool> IDataAccessService<Status, int>.Create(Status entity)
        {
            throw new NotImplementedException();
        }

        Task<bool> IDataAccessService<Status, int>.DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        async Task<List<Status>> IDataAccessService<Status, int>.GetDataAsync()
        {
            try
            {
                var statusList = await dbAccess.Status.ToListAsync();
                return statusList;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        Task<Status> IDataAccessService<Status, int>.GetDataAsync(int id)
        {
            throw new NotImplementedException();
        }

        Task<bool> IDataAccessService<Status, int>.UpdateAsync(Status entity, int id)
        {
            throw new NotImplementedException();
        }
    }
}