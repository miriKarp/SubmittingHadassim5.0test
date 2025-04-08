using AutoMapper;
using Microsoft.EntityFrameworkCore;
using StoreManagement.DTO;
using StoreManagement.Models;
using StoreManagement.Repo.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreManagement.Repo.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly StoreManagementContext _context;
        private readonly IMapper _mapper;

        public ProductRepository(StoreManagementContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ProductDTO>> GetAllProductsAsync()
        {
            var products = await _context.Products.ToListAsync();
            return _mapper.Map<IEnumerable<ProductDTO>>(products); 

         // return await _context.Products.ToListAsync();
        }

        public async Task<ProductDTO> GetProductByIdAsync(int id)
        {
            var product = await _context.Products.FindAsync(id);
            return _mapper.Map<ProductDTO>(product); // המרה מ-Product ל-ProductDTO

           // return await _context.Products.FindAsync(id);
        }

        public async Task AddProductAsync(ProductDTO productDTO)
        {
            var product = _mapper.Map<Product>(productDTO);

            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            //_context.Products.Add(product);
            //await _context.SaveChangesAsync();
        }
    }
}