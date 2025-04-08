using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StoreManagement.DTO;
using StoreManagement.Models;
using StoreManagement.Repo.IRepositories;

[Route("api/[controller]")]
[ApiController]
public class OrdersController : ControllerBase
{
    private readonly IOrderRepository _orderRepository;

    public OrdersController(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    //[Authorize(Roles = "Manager")]
    [HttpGet("GetAllOrders")]
    public async Task<IActionResult> GetAllOrders()
    {
        try
        {
            var orders = await _orderRepository.GetAllOrdersAsync();
            return Ok(orders);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }
    //[Authorize(Roles = "Supplier")]
    [HttpGet("GetOrdersBySupplierId/{supplierId}")]
    public async Task<IActionResult> GetOrdersBySupplierId(int supplierId)
    {
        try
        {
            var orders = await _orderRepository.GetOrdersBySupplierIdAsync(supplierId);
            if (orders == null || !orders.Any())
            {
                return NotFound($"No orders found for supplier with ID {supplierId}.");
            }

            return Ok(orders);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    //[Authorize(Roles = "Manager")]
    //[Authorize(Roles = "Supplier")]
    [HttpGet("GetOrderById/{id}")]
    public async Task<IActionResult> GetOrderById(int id)
    {
        try
        {
            var order = await _orderRepository.GetOrderByIdAsync(id);
            if (order == null)
            {
                return NotFound($"Order with ID {id} not found.");
            }
            return Ok(order);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message} ");
        }
    }

    //[Authorize(Roles = "Manager")]
    [HttpPost("CreateOrder")]
    public async Task<IActionResult> AddOrder([FromBody] OrderDTO order)
    {
        if (order == null)
        {
            return BadRequest("Order data is null.");
        }
        try
        {
            await _orderRepository.AddOrderAsync(order);
            return CreatedAtAction(nameof(GetOrderById), new { id = order.Id }, order);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message} ");
        }
    }

    //[Authorize(Roles = "Supplier")]
    [HttpPut("approveBySupplier/{id}")]
    public async Task<IActionResult> ApproveOrderBySupplier(int id)
    {
        try
        {
            var order = await _orderRepository.GetOrderByIdAsync(id);
            if (order == null)
            {
                return NotFound($"Order with ID {id} not found.");
            }
            if (order.Status != OrderStatus.Pending)
            {
                return BadRequest($"Order with ID {id} is not in Pending status.");
            }
            order.Status = OrderStatus.InProcess; 
            await _orderRepository.UpdateOrderAsync(order);
            return Ok(order);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message} ");
        }
    }

    //[Authorize(Roles = "Manager")]
    [HttpPut("approveByStoreOwner/{id}")]
    public async Task<IActionResult> ApproveOrderByStoreOwner(int id)
    {
        try
        {
            var order = await _orderRepository.GetOrderByIdAsync(id);
            if (order == null)
            {
                return NotFound($"Order with ID {id} not found.");
            }

            if (order.Status != OrderStatus.InProcess)
            {
                return BadRequest($"Order with ID {id} is not in InProcess status.");
            }

            order.Status = OrderStatus.Completed; 
            await _orderRepository.UpdateOrderAsync(order);
            return Ok(order);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message} ");
        }
    }
}
