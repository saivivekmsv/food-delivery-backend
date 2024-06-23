using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication1;
[Route("api/v1/[controller]")]
[ApiController]
[Authorize]
public class TestController: Controller
{
    private readonly ILogger<TestController> _logger;
    public TestController(ILogger<TestController> logger)
    {
        _logger = logger;
    }
    [HttpGet]
    public async Task<int> Get(int id)
    {
        return 1234;
    }

}
