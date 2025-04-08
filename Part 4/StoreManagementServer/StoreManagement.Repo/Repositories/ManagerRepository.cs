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
    public class ManagerRepository : IManagerRepository
    {
        private readonly StoreManagementContext _context;
        private readonly IMapper _mapper;

        public ManagerRepository(StoreManagementContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ManagerDTO> GetManagerAsync()
        {
            var manager = await _context.Manager.FirstOrDefaultAsync();
            if (manager == null)
                return null;
            return _mapper.Map<ManagerDTO>(manager);
        }
        public async Task<ManagerDTO> AddAsync(ManagerDTO managerDTO)
        {
            var manager = _mapper.Map<Manager>(managerDTO);
            _context.Manager.Add(manager);
            await _context.SaveChangesAsync();
            var managerDto = _mapper.Map<ManagerDTO>(manager);
            managerDto.Id = manager.Id;
            return managerDto;
        }
        public async Task UpdateAsync(ManagerDTO managerDTO)
        {
            var manager = _mapper.Map<Manager>(managerDTO);
            _context.Manager.Update(manager);
            await _context.SaveChangesAsync();
        }

    }
}
