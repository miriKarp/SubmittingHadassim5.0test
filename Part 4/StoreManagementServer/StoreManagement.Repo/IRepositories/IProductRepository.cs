using StoreManagement.DTO;
using StoreManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreManagement.Repo.IRepositories
{
    public interface IProductRepository
    {
        public Task<IEnumerable<ProductDTO>> GetAllProductsAsync();
        public Task<ProductDTO> GetProductByIdAsync(int id);
        public Task AddProductAsync(ProductDTO productDTO);
    }
}
