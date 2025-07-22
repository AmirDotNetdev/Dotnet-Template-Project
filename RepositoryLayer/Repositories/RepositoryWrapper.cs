using RepositoryLayer.Data;
using RepositoryLayer.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Repositories
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private string _errorMessage = string.Empty;
        public RepositoryWrapper(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        public int SaveChanges()
        {
            return _applicationDbContext.SaveChanges();
        }

        public async Task<int> SaveChangesAsync()
        {
            try
            {
                return await _applicationDbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                throw new Exception("Cant Save Db",ex);
            }
        }
    }
}
