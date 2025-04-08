using AutoMapper;
using Microsoft.EntityFrameworkCore;
using StoreManagement.Models;
using StoreManagement.DTO;
using StoreManagement.Repo.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreManagement.Repo.Repositories
{
    public class SupplierRepository : ISupplierRepository
    {
        private readonly StoreManagementContext _context;
        private readonly IMapper _mapper;
        public SupplierRepository(StoreManagementContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<SupplierDTO>> GetAllSuppliersAsync()
        {
            var suppliers = await _context.Suppliers
            .Include(s => s.SupplierProduct)
            .ThenInclude(sp => sp.Product) 
            .ToListAsync();
            return _mapper.Map<IEnumerable<SupplierDTO>>(suppliers); 
        }

        public async Task<SupplierDTO> GetSupplierByIdAsync(int id)
        {
            var supplier = await _context.Suppliers.Include(s => s.SupplierProduct).FirstOrDefaultAsync(s => s.Id == id);
            return _mapper.Map<SupplierDTO>(supplier); 
        }

        public async Task<int> AddSupplierAsync(SupplierDTO supplierDTO)
        {
            var supplier = _mapper.Map<Supplier>(supplierDTO);

            if (supplierDTO.SupplierProducts != null && supplierDTO.SupplierProducts.Any())
            {
                foreach (var productDTO in supplierDTO.SupplierProducts)
                {
                    var existingProduct = await _context.Products.FirstOrDefaultAsync(p => p.Name == productDTO.ProductName);
                    Product product;
                    if (existingProduct != null)
                    {
                        product = existingProduct;
                    }
                    else
                    {
                        product = new Product { Name = productDTO.ProductName };
                        _context.Products.Add(product);
                        await _context.SaveChangesAsync(); 
                    }
                    supplier.SupplierProduct.Add(new SupplierProduct
                    {
                        Supplier = supplier,
                        ProductId = product.Id, 
                        ProductName = productDTO.ProductName, 
                        PricePerItem = productDTO.PricePerItem,
                        MinimumQuantity = productDTO.MinimumQuantity
                    });
                }
            }

            var newSupplier = _context.Suppliers.Add(supplier); 
            await _context.SaveChangesAsync();
            return newSupplier.Entity.Id;
        }
        public async Task<SupplierDTO> GetSupplierByPhoneNumberAsync(string phoneNumber)
        {
            var supplier = await _context.Suppliers
                .Include(s => s.SupplierProduct)
                .ThenInclude(sp => sp.Product) 
                .FirstOrDefaultAsync(s => s.PhoneNumber == phoneNumber);
            if (supplier == null)
            {
                return null; 
            }
            return _mapper.Map<SupplierDTO>(supplier);
        }

    }
}
