//using Microsoft.AspNetCore.Mvc;
//using static StoreManagement.DTO.SigninDTO;
//using StoreManagement.API.Services;
//using StoreManagement.Models;

//namespace StoreManagement.API.Controllers
//{
//    [ApiController]
//    [Route("api/[controller]")]
//    public class AuthController : ControllerBase
//    {
//        private readonly StoreManagementContext _context;
//        private readonly JwtService _jwtService;

//        public AuthController(StoreManagementContext context, JwtService jwtService)
//        {
//            _context = context;
//            _jwtService = jwtService;
//        }

//        [HttpPost("login")]
//        public IActionResult Login([FromBody] LoginDto loginDto)
//        {
//            if (loginDto.Role == "Manager")
//            {
//                var manager = _context.Manager.FirstOrDefault();
//                if (manager != null && manager.Password == loginDto.Password)
//                {
//                    var token = _jwtService.GenerateToken(manager.Id.ToString(), "Manager");
//                    return Ok(new { token });
//                }
//            }
//            else if (loginDto.Role == "Supplier")
//            {
//                var supplier = _context.Suppliers
//                    .FirstOrDefault(s => s.CompanyName == loginDto.Username && s.Password == loginDto.Password);
//                if (supplier != null)
//                {
//                    var token = _jwtService.GenerateToken(supplier.Id.ToString(), "Supplier");
//                    return Ok(new { token });
//                }
//            }

//            return Unauthorized("Invalid credentials");
//        }
//    }

//}
