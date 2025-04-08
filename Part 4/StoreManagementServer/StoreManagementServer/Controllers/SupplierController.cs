using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StoreManagement.DTO;
using StoreManagement.Models;
using StoreManagement.Repo.IRepositories;
using BCrypt.Net; 
using StoreManagement.Repo.Repositories;

namespace StoreManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SupplierController : ControllerBase
    {
        private readonly ISupplierRepository _supplierRepository;
        //private readonly JwtService _JwtService;

        public SupplierController(ISupplierRepository supplierRepository)
        {
            _supplierRepository = supplierRepository;
        }
        //[Authorize(Roles = "Manager")]
        [HttpGet("GetAllSuppliers")]
        public async Task<ActionResult<IEnumerable<Supplier>>> GetSuppliers()
        {
            try
            {
                var suppliers = await _supplierRepository.GetAllSuppliersAsync();
                return Ok(suppliers);

            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        //[Authorize(Roles = "Manager")]
        [HttpGet("GetSupplierById/{id}")]
        public async Task<IActionResult> GetSupplierById(int id)
        {
            try
            {
                var supplier = await _supplierRepository.GetSupplierByIdAsync(id);
                if (supplier == null)
                    return NotFound($"supplier with ID {id} not found.");
                return Ok(supplier);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message} ");
            }
        }

        [HttpPost("CreateSupplier")]
        public async Task<ActionResult<int>> Create([FromBody] SupplierDTO supplier)
        {
            if (supplier == null)
                return BadRequest("Supplier data is null.");
            await _supplierRepository.AddSupplierAsync(supplier);
            return CreatedAtAction(nameof(GetSuppliers), new { }, supplier);

        }

        //[HttpPost("Register")] 
        //public async Task<IActionResult> Register([FromBody] SupplierDTO registerDto)
        //{
        //    if (registerDto == null || string.IsNullOrEmpty(registerDto.PhoneNumber) || string.IsNullOrEmpty(registerDto.Password))
        //    {
        //        return BadRequest("Invalid registration data.");
        //    }

        //    var existingSupplier = await _supplierRepository.GetSupplierByPhoneNumberAsync(registerDto.PhoneNumber);
        //    if (existingSupplier != null)
        //    {
        //        return Conflict("Supplier with this phone number already exists.");
        //    }
            
        //    registerDto.Password = HashPassword(registerDto.Password);
        //    try
        //    {
        //        await _supplierRepository.AddSupplierAsync(registerDto);

        //        if (_JwtService != null)
        //        {
        //            var token = _JwtService.GenerateToken(registerDto.Id.ToString(),"Supplier");
        //            return Ok(new { Token = token, SupplierId = registerDto.Id, PhoneNumber = registerDto.PhoneNumber, CompanyName = registerDto.CompanyName });
        //        }
        //        else
        //        {
        //            return StatusCode(201); 
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, $"Internal server error during registration: {ex.Message}");
        //    }
        //}

        //private string HashPassword(string password)
        //{
        //    return BCrypt.Net.BCrypt.HashPassword(password);
        //}
    }
}

