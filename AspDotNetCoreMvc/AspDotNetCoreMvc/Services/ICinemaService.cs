using AspDotNetCoreMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspDotNetCoreMvc.Services
{
    public interface ICinemaService
    {
        Task<IEnumerable<Cinema>> GetllAllAsync();
        Task<Cinema> GetByIdAsync(int id);
        Task AddAsync(Cinema model);
    }
}
