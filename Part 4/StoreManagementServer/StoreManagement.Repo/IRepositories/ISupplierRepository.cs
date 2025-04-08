using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StoreManagement.DTO;
using StoreManagement.Models;

namespace StoreManagement.Repo.IRepositories
{
    public interface ISupplierRepository
    {
        public Task<IEnumerable<SupplierDTO>> GetAllSuppliersAsync();
        public Task<SupplierDTO> GetSupplierByIdAsync(int id);
        public Task<int> AddSupplierAsync(SupplierDTO supplierDTO);
        public Task<SupplierDTO> GetSupplierByPhoneNumberAsync(string phoneNumber);
    }
}
