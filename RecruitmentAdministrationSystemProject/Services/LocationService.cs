using RecruitmentAdministrationSystemProject.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace RecruitmentAdministrationSystemProject.Services
{
    public class LocationService : IDataAccessService<Location, int>
    {
        RecruitmentManagementSystemEntities dbAccess;
        public LocationService(RecruitmentManagementSystemEntities dbAccess)
        {
            this.dbAccess = dbAccess;
        }
        Task<Location>IDataAccessService<Location, int>.Create(Location entity)
        {
            throw new NotImplementedException();
        }

        Task<bool> IDataAccessService<Location, int>.DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        async Task<List<Location>> IDataAccessService<Location, int>.GetDataAsync()
        {
            try
            {
                var locations =await dbAccess.Locations.ToListAsync();
                return locations;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        Task<Location> IDataAccessService<Location, int>.GetDataAsync(int id)
        {
            throw new NotImplementedException();
        }

        Task<bool> IDataAccessService<Location, int>.UpdateAsync(Location entity, int id)
        {
            throw new NotImplementedException();
        }
    }
}