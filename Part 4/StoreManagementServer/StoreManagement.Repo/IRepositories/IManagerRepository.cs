using StoreManagement.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreManagement.Repo.IRepositories
{
    public interface IManagerRepository
    {
        Task<ManagerDTO> GetManagerAsync();
        Task<ManagerDTO> AddAsync(ManagerDTO manager);
        Task UpdateAsync(ManagerDTO manager);
    }
}
