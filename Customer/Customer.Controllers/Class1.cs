using Microsoft.AspNetCore.Mvc;

namespace Customer.Controllers;
[Route("api/v1/[controller]")]
[ApiController]
public class CustomerControllers : Controller
{
    [HttpGet("GetCustomer")]
    public async Task<Customer.Models.Customer> GetCustomer(int id)
    {
        var customer = new Models.Customer() { Name = "vivek",PhoneNumber=990888};
        return customer;
    }
    [HttpGet("GetCustomerOrders")]
    public async Task<IActionResult> GetCustomerOrders(int id)
    {
        return Ok();
    }
}
